using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Networking.Transport;
using UnityEngine;

namespace MultiplayPingSample.Server
{
    public class GameServerUdpPingHandler : IDisposable
    {
        /// <summary>
        ///     Whether or not the Ping Handler has received a shutdown signal
        /// </summary>
        public bool ShouldShutdown { get; private set; }

        /// <summary>
        ///     The number of active client connections to the server
        /// </summary>
        public ushort ConnectedClients { get; private set; }

        /// <summary>
        ///     The maximum number of clients that can be connected to this GameServerUdpPingHandler
        /// </summary>
        public ushort MaxConnections { get; private set; }

        /// <summary>
        ///     The IP address this GameServerUdpPingHandler is bound to
        /// </summary>
        public string IpAddress { get; private set; }

        /// <summary>
        ///     The port this GameServerUdpPingHandler is bound to
        /// </summary>
        public ushort Port { get; private set; }

        NetworkEndPoint m_PingServerEndpoint;
        NativeArray<bool> m_ShouldShutdownServer;
        NativeList<NetworkConnection> m_Connections;
        UdpNetworkDriver m_ServerDriver;
        JobHandle m_UpdateHandle;
        bool m_Initialized;
        bool m_Disposed;
        bool m_ShuttingDown;
        bool InTerminalState => m_Disposed || m_ShuttingDown;

        public void Update()
        {
            if (InTerminalState)
                return;

            // Don't do anything more unless our jobs have been completed
            if (!m_UpdateHandle.IsCompleted)
                return;

            // Wait for the previous frames ping to complete before starting a new one
            m_UpdateHandle.Complete();

            // Update the network drivers and our list of active connections
            WaitForNetworkUpdate();

            // Update the number of connections
            if (m_Connections.IsCreated && m_ServerDriver.IsCreated)
            {
                ushort activeConnections = 0;

                for (int i = 0; i < m_Connections.Length; i++)
                {
                    if (m_Connections[i].IsCreated
                        && m_Connections[i].GetState(m_ServerDriver) != NetworkConnection.State.Disconnected)
                        activeConnections++;
                }

                ConnectedClients = activeConnections;
            }
            else
            {
                ConnectedClients = (ushort)0;
            }

            // Set the "Hey we got a shutdown signal" flag
            // It's up to the developer whether or not they want to respect this flag
            if (m_ShouldShutdownServer[0])
            {
                ShouldShutdown = true;
            }

            SchedulePongJob();

            // Put our jobs on the stack to be processed without waiting for completion in this frame
            JobHandle.ScheduleBatchedJobs();
        }

        void WaitForNetworkUpdate()
        {
            if (InTerminalState)
                return;

            // The DriverUpdateJob which accepts new connections should be the second job in the chain,
            //  it needs to depend on the driver update job
            var updateJob = new DriverUpdateJob
            {
                driver = m_ServerDriver,
                connections = m_Connections
            };

            // Update the driver should be the first job in the chain
            m_UpdateHandle = m_ServerDriver.ScheduleUpdate(m_UpdateHandle);
            m_UpdateHandle = updateJob.Schedule(m_UpdateHandle);

            // Wait for the job to complete
            m_UpdateHandle.Complete();
        }

        // Schedule a pong job, which looks for pings and replies to them
        void SchedulePongJob()
        {
            if (InTerminalState)
                return;

            var pongJob = new PongJob
            {
                // Check to see if we need to shut down after processing data
                shouldShutdown = m_ShouldShutdownServer,

                // PongJob is a ParallelFor job, it must use the concurrent NetworkDriver
                driver = m_ServerDriver.ToConcurrent(),

                // IJobParallelForDeferExtensions is not working correctly with IL2CPP
                connections = m_Connections
            };

            // PongJob uses IJobParallelForDeferExtensions, we *must* schedule with a list as first parameter rather than
            // an int since the job needs to pick up new connections from DriverUpdateJob
            // The PongJob is the last job in the chain and it must depends on the DriverUpdateJob
            m_UpdateHandle = pongJob.Schedule(m_UpdateHandle);
        }

        public void ShutDown()
        {
            DisconnectClients();
            Dispose();
        }

        public void Dispose()
        {
            if (m_Disposed)
                return;

            // All jobs must be completed before we can dispose the data they use
            m_UpdateHandle.Complete();

            // Dispose resources
            m_ServerDriver.Dispose();
            m_Connections.Dispose();
            m_ShouldShutdownServer.Dispose();

            m_Disposed = true;
        }

        ~GameServerUdpPingHandler()
        {
            Dispose();
        }

        // Disconnect all clients and shut down the server
        void DisconnectClients()
        {
            if (InTerminalState)
                return;

            m_ShuttingDown = true;

            //Debug.Log("Server detected remote shutdown signal.  Disconnecting all clients and shutting down server.");
            Debug.Log("Disconnecting all clients and shutting down UDP ping server.");

            var disconnectJob = new DisconnectAllClientsJob
            {
                driver = m_ServerDriver,
                connections = m_Connections
            };

            // Finish up all other active jobs
            m_UpdateHandle.Complete();

            // Schedule a new disconnect job
            m_UpdateHandle = disconnectJob.Schedule();

            // Update the driver after the disconnect
            m_UpdateHandle = m_ServerDriver.ScheduleUpdate(m_UpdateHandle);

            // Wait for everything to finish
            m_UpdateHandle.Complete();
        }

        public GameServerUdpPingHandler(string ipAddress, ushort port, ushort maxConnections = 0)
        {
            // Try to create a valid endpoint from the included IP and port
            if (!NetworkEndPoint.TryParse(ipAddress, port, out m_PingServerEndpoint))
                throw new ArgumentException($"The provided endpoint ({ipAddress}:{port}) is not a valid endpoint");

            // Update properties
            IpAddress = ipAddress;
            Port = port;
            MaxConnections = maxConnections > 0 ? maxConnections : (ushort)1;

            Initialize();
        }

        // Create the ping server driver, bind it to a port and start listening for incoming connections
        void Initialize()
        {
            if (m_Initialized || InTerminalState)
                return;

            m_ServerDriver = new UdpNetworkDriver(new INetworkParameter[0]);

            var bindStatus = m_ServerDriver.Bind(m_PingServerEndpoint);
            if (bindStatus != 0)
                throw new InvalidOperationException($"Failed to bind to {IpAddress ?? "0.0.0.0"}:{Port}.  Bind error code: {bindStatus}.");

            Debug.Log($"Network driver listening for traffic on {IpAddress ?? "0.0.0.0"}:{Port}");

            m_ServerDriver.Listen();

            
            m_Connections = new NativeList<NetworkConnection>(MaxConnections, Allocator.Persistent);

            m_ShouldShutdownServer = new NativeArray<bool>(1, Allocator.Persistent)
            {
                [0] = false
            };

            m_Initialized = true;
        }

        [BurstCompile]
        struct DriverUpdateJob : IJob
        {
            public UdpNetworkDriver driver;
            public NativeList<NetworkConnection> connections;

            public void Execute()
            {
                // Remove connections which have been destroyed from the list of active connections
                for (var i = 0; i < connections.Length; ++i)
                    if (!connections[i].IsCreated)
                    {
                        connections.RemoveAtSwapBack(i);
                        // Index i is a new connection since we did a swap back, check it again
                        --i;
                    }

                // Accept all new connections
                while (true)
                {
                    var con = driver.Accept();

                    // "Nothing more to accept" is signaled by returning an invalid connection from accept
                    if (!con.IsCreated)
                        break;

                    connections.Add(con);
                }
            }
        }

        [BurstCompile]
        struct DisconnectAllClientsJob : IJob
        {
            public UdpNetworkDriver driver;
            public NativeList<NetworkConnection> connections;

            public void Execute()
            {
                for (var i = 0; i < connections.Length; ++i)
                    connections[i].Disconnect(driver);
            }
        }
        
        [BurstCompile]
        struct PongJob : IJob
        {
            public UdpNetworkDriver.Concurrent driver;
            public NativeList<NetworkConnection> connections;

            [WriteOnly]
            public NativeArray<bool> shouldShutdown;

            public void Execute()
            {
                for (var i = 0; i < connections.Length; ++i)
                {
                    connections[i] = ProcessSingleConnection(driver, connections[i], out var shouldShutdownServer);

                    if (shouldShutdownServer)
                        shouldShutdown[0] = true;
                }
            }
        }

        static NetworkConnection ProcessSingleConnection(UdpNetworkDriver.Concurrent driver, NetworkConnection connection, out bool terminateServer)
        {
            terminateServer = false;
            NetworkEvent.Type cmd;

            // Pop all events for the connection
            while ((cmd = driver.PopEventForConnection(connection, out var dataStreamReader)) != NetworkEvent.Type.Empty)
                if (cmd == NetworkEvent.Type.Data)
                {
                    // For ping requests we reply with a pong message
                    // A DataStreamReader.Context is required to keep track of current read position since
                    // DataStreamReader is immutable
                    var readerCtx = default(DataStreamReader.Context);
                    var id = dataStreamReader.ReadInt(ref readerCtx);

                    // Terminate server if "magic number" received
                    if (id == -1)
                        terminateServer = true;

                    // Create a temporary DataStreamWriter to keep our serialized pong message
                    var pongData = new DataStreamWriter(4, Allocator.Temp);
                    pongData.Write(id);

                    // Send the pong message with the same id as the ping
                    driver.Send(NetworkPipeline.Null, connection, pongData);
                }
                else if (cmd == NetworkEvent.Type.Disconnect)
                {
                    // When disconnected we make sure the connection return false to IsCreated so the next frames
                    // DriverUpdateJob will remove it
                    return default(NetworkConnection);
                }

            return connection;
        }
    }
}
