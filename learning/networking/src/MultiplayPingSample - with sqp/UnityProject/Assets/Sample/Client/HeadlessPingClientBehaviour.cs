using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Unity.Ucg.MmConnector;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace MultiplayPingSample.Client
{
    public class HeadlessPingClientBehaviour : MonoBehaviour
    {
        // Explicitly defined exit codes
        public enum ExitCode
        {
            Ok = 0,
            Error = 1
        }

        // Inspector properties & defaults
        public string CustomIp = "127.0.0.1:9000";
        public bool ForceHeadlessMode = false;
        public ushort HeadlessRunTimeMs = 5000;
        public bool HeadlessShouldPingServer = true;
        public bool HeadlessShouldTerminateServer = false;
        public string MatchmakingServer = "";
        public uint MatchmakingTimeoutMs = 5 * 60 * 1000;
        public string FleetId = "";

        // Internal fields
        string m_CustomIp;
        string m_FleetId;
        ushort m_HeadlessRunTimeMs;
        bool m_HeadlessShouldMatchmake;
        bool m_HeadlessShouldPingServer;
        bool m_HeadlessShouldQos;
        bool m_HeadlessShouldTerminateServer;
        string m_MatchmakingServer;
        uint m_MatchmakingTimeoutMs;
        IList<QosResultMultiplay> m_QosResults;

        // Monobehaviour method called before first Update() call
        void Start()
        {
            // Only run the headless lifecycle if actually in headless mode
            if (ShutdownIfNotHeadless())
                return;

            // Try to get arguments from GameObject first (overrides defaults set in code)
            ReadArgumentsFromGameObjectProperties();

            // Try to get arguments from commandline second (overrides GameObject values)
            ReadArgumentsFromCommandline();

            // Validate that all the updated arguments are actually valid
            ValidateArguments();

            // Start the ping client lifecycle
            StartCoroutine(HeadlessClientLifecycle());
        }

        bool ShutdownIfNotHeadless()
        {
            if (ForceHeadlessMode
                || CommandLine.HasArgument("-nographics")
                || CommandLine.HasArgument("-batchmode"))
            {
                Debug.Log("Running Ping Client in headless mode.");
                return false;
            }

            Destroy(this);
            return true;
        }

        // Copy GameObject arguments to internal fields
        void ReadArgumentsFromGameObjectProperties()
        {
            m_CustomIp = CustomIp;
            m_HeadlessRunTimeMs = HeadlessRunTimeMs;
            m_HeadlessShouldPingServer = HeadlessShouldPingServer;
            m_HeadlessShouldTerminateServer = HeadlessShouldTerminateServer;
            m_MatchmakingServer = MatchmakingServer;
            m_MatchmakingTimeoutMs = MatchmakingTimeoutMs;
            m_FleetId = FleetId;
        }

        // Try to update arguments based on commandline args
        void ReadArgumentsFromCommandline()
        {
            // Process args
            m_HeadlessShouldPingServer = m_HeadlessShouldPingServer || CommandLine.HasArguments("-p", "-ping");
            m_HeadlessShouldTerminateServer = m_HeadlessShouldTerminateServer || CommandLine.HasArguments("-k", "-kill");

            CommandLine.TryUpdateVariableWithArgValue(ref m_CustomIp, "-e", "-endpoint");
            CommandLine.TryUpdateVariableWithArgValue(ref m_HeadlessRunTimeMs, "-t", "pingtime");
            CommandLine.TryUpdateVariableWithArgValue(ref m_MatchmakingServer, "-mm", "-mmserver");
            CommandLine.TryUpdateVariableWithArgValue(ref m_MatchmakingTimeoutMs, "-mmtimeout");

            // Only do qos if a fleet has been provided
            CommandLine.TryUpdateVariableWithArgValue(ref m_FleetId, "-fleet");
            m_HeadlessShouldQos = m_HeadlessShouldQos || !string.IsNullOrEmpty(m_FleetId);
        }

        void ValidateArguments()
        {
            m_HeadlessShouldMatchmake = !string.IsNullOrEmpty(m_MatchmakingServer);

            // Cannot have both a custom IP and matchmaking set
            var hasEndpoint = !string.IsNullOrEmpty(m_CustomIp);
            var hasMatchmaker = m_HeadlessShouldMatchmake;

            if (hasEndpoint && hasMatchmaker || !hasEndpoint && !hasMatchmaker)
            {
                Debug.LogError("Ping client started in headless mode, but was not provided with the proper arguments.\n" +
                    "You must specify either an endpoint or a matchmaker.");

                ShutDown(ExitCode.Error);
            }

            // Must at least ping or terminate
            if (!m_HeadlessShouldPingServer && !m_HeadlessShouldTerminateServer)
            {
                Debug.LogError("Ping client started in headless mode, but was not provided with the proper arguments.\n" +
                    "You must specify an operation: either ping (-p), terminate (-k), or both.");

                Debug.Log("-p : " + m_HeadlessShouldPingServer);
                Debug.Log("-k : " + m_HeadlessShouldTerminateServer);
                Debug.Log("-endpoint : " + m_CustomIp);

                ShutDown(ExitCode.Error);
            }
        }

        IEnumerator HeadlessClientLifecycle()
        {
            if (m_HeadlessShouldQos)
                yield return DoQosAsync();

            if (m_HeadlessShouldMatchmake)
                yield return DoMatchmakingAsync();

            if (m_HeadlessShouldPingServer)
                yield return DoPingAsync();

            if (m_HeadlessShouldTerminateServer)
                UdpPingWrapper.TryTerminateRemoteServer(m_CustomIp);

            Debug.Log("Finished headless mode tasks, shutting down...");
            ShutDown(ExitCode.Ok);
        }

        IEnumerator DoQosAsync()
        {
            // Qos Discovery - Find servers to ping
            var qosDiscovery = new QosDiscoveryAsyncWrapper();
            yield return qosDiscovery.StartEnumerator(m_FleetId);
            var servers = qosDiscovery.Servers;

            if (servers == null || servers.Length == 0)
            {
                Debug.LogWarning("Qos was requested, but no qos servers could be found.");
                yield break;
            }

            // Qos Pings - Ping servers for info
            var qosPinger = new QosPingAsyncWrapper(servers);
            yield return qosPinger.StartEnumerator();
            m_QosResults = qosPinger.QosResults;

            if (m_QosResults == null || m_QosResults.Count == 0)
                Debug.LogWarning("Qos was requested, but no valid qos results were returned.");
            else
                QosConnector.Instance.RegisterProvider(() => m_QosResults);
        }

        IEnumerator DoMatchmakingAsync()
        {
            // Create timeout cancellation token
            var token = CancellationToken.None;
            if (MatchmakingTimeoutMs > 0)
            {
                token = new CancellationTokenSource((int)MatchmakingTimeoutMs).Token;
            }

            var matchmaking = new MatchmakingWrapper(m_MatchmakingServer);

            yield return matchmaking.StartEnumerator(serverEndpoint =>
            {
                if (string.IsNullOrEmpty(serverEndpoint))
                {
                    Debug.Log("Matchmaking failed, aborting headless run and shutting down ping client.");
                    ShutDown(ExitCode.Error);
                }

                // Success!
                m_CustomIp = serverEndpoint;
            }, token);
        }

        IEnumerator DoPingAsync()
        {
            // How long to run the ping
            var pingSeconds = m_HeadlessRunTimeMs / 1000f;
            Debug.Log($"Pinging remote server for {pingSeconds} seconds...");

            // Start new ping client
            var udpPinger = new UdpPingWrapper();
            var timer = Stopwatch.StartNew();
            try
            {
                udpPinger.Start(m_CustomIp);

                while (timer.ElapsedMilliseconds < m_HeadlessRunTimeMs)
                {
                    udpPinger.Update();
                    yield return null;
                }

                // Dump ping stats before we dispose of the client
                Debug.Log(udpPinger.GetStats());
            }
            finally
            {
                udpPinger.Dispose();
            }
        }

        void ShutDown(ExitCode exitCode)
        {
            StopAllCoroutines();
            PingUtilities.EndProgram((int)exitCode);
        }
    }
}
