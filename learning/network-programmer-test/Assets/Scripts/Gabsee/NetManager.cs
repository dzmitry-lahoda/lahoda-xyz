using System;
using System.Net;
using System.Net.Sockets;

namespace Gabsee
{
    public partial class NetManager
    {
        private NetManagerInternal m_netManager = new NetManagerInternal();

        /// <summary>
        /// Network error (on send or receive)
        /// </summary>
        public Action<IPEndPoint, SocketError> OnNetworkError
        {
            set { m_netManager.OnNetworkErrorCallback = value; }
        }

        /// <summary>
        /// Received some data
        /// </summary>
        public Action<IPEndPoint, byte[]> OnNetworkReceive
        {
            set { m_netManager.OnNetworkReceiveCallback = value; }
        }

        /// <summary>
        /// New remote peer connected to host, or client connected to remote host
        /// </summary>
        public Action<IPEndPoint> OnConnection
        {
            set { m_netManager.OnConnectionCallback = value; }
        }

        /// <summary>
        /// Peer disconnected
        /// </summary>
        public Action<IPEndPoint> OnDisconnection
        {
            set { m_netManager.OnDisconnectionCallback = value; }
        } 

        /// <summary>
        /// Listening on selected port
        /// </summary>
        /// <param name="p_port">port to listen</param>
        /// <returns></returns>
        public bool StartServer(int p_port) => m_netManager.StartServer(p_port);

        /// <summary>
        /// Listening on available port
        /// And connect to remote host
        /// </summary>
        /// <param name="p_address">Server IP or hostname</param>
        /// <param name="p_port">Server Port</param>
        /// <returns></returns>
        public bool StartClient(string p_address, int p_port) => m_netManager.StartClient(p_address, p_port);

        /// <summary>
        /// Force closes connection
        /// </summary>
        public void Stop() => m_netManager.Stop();

        /// <summary>
        /// Receive all pending events. Call this in game update code
        /// </summary>
        public void Update() => m_netManager.Update();

        /// <summary>
        /// Send data to all connected peers
        /// </summary>
        /// <param name="p_data">data</param>
        public void Send(byte[] p_data) => m_netManager.Send(p_data);

        /// <summary>
        /// Send data to peer
        /// </summary>
        /// <param name="p_endPoint">Peer endpoint</param>
        /// <param name="p_data">Data</param>
        public void Send(IPEndPoint p_endPoint, byte[] p_data) => m_netManager.Send(p_endPoint, p_data);

    }
}