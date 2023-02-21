using System;
using System.Net;
using Unity.Ucg.Usqp;
using UnityEngine;

namespace MultiplayPingSample.Server
{
    // SQP Wrapper
    public class MultiplayServerStatusQueryHandler : IDisposable
    {
        UsqpServer m_SqpServer;
        bool m_Disposed;

        public ServerInfo.Data Info
        {
            get => m_SqpServer.ServerInfoData;
            set => m_SqpServer.ServerInfoData = value;
        }

        public void Update()
        {
            if (m_Disposed)
                return;

            m_SqpServer?.Update();
        }

        public MultiplayServerStatusQueryHandler(string ipAddress, ushort port)
        {
            var address = IPAddress.Parse(ipAddress);
            var endpoint = new IPEndPoint(address, port);
            m_SqpServer = new UsqpServer(endpoint);
        }

        public void Dispose()
        {
            m_Disposed = true;
            m_SqpServer?.Dispose();
        }
    }
}
