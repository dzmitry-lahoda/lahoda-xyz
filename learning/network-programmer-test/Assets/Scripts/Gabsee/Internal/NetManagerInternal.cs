using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using LiteNetLib;

namespace Gabsee
{
    public partial class NetManager
    {
        private class NetManagerInternal : LiteNetLib.INetEventListener
        {
            private LiteNetLib.NetManager m_liteNetLibNetManager;
            private Dictionary<IPEndPoint, NetPeer> m_connectedPeer = new Dictionary<IPEndPoint, NetPeer>();
            private LiteNetLib.Utils.NetDataWriter m_writer = new LiteNetLib.Utils.NetDataWriter();

            public Action<IPEndPoint , SocketError> OnNetworkErrorCallback;
            public Action<IPEndPoint , byte[]> OnNetworkReceiveCallback;
            public Action<IPEndPoint> OnConnectionCallback;
            public Action<IPEndPoint> OnDisconnectionCallback;

            public NetManagerInternal()
            {
                m_liteNetLibNetManager = new LiteNetLib.NetManager(this);
            }

            public bool StartServer(int p_port)
            {
                return m_liteNetLibNetManager.Start(p_port);
            }

            public bool StartClient(string p_address, int p_port)
            {
                if (!m_liteNetLibNetManager.Start())
                    return false;
                m_liteNetLibNetManager.Connect(p_address, p_port, "Gabsee");
                return true;
            }

            public void Stop()
            {
                m_liteNetLibNetManager.Stop();
                m_connectedPeer.Clear();
            }

            public void Update()
            {
                m_liteNetLibNetManager.PollEvents();
            }

            public void Send(IPEndPoint p_endPoint, byte[] p_data)
            {
                if (m_connectedPeer.TryGetValue(p_endPoint, out NetPeer peer))
                {
                    m_writer.Reset();
                    m_writer.PutBytesWithLength(p_data);
                    peer.Send(m_writer, DeliveryMethod.Unreliable);
                }
            }

            public void Send(byte[] p_data)
            {
                m_writer.Reset();
                m_writer.PutBytesWithLength(p_data);
                m_liteNetLibNetManager.SendToAll(m_writer, DeliveryMethod.Unreliable);
            }

            public void OnConnectionRequest(ConnectionRequest request)
            {
                request.AcceptIfKey("Gabsee");
            }

            public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
            {
#if NET_DEBUG
                UnityEngine.Debug.LogError($"OnNetworkError: {endPoint} {socketError}");
#endif
                OnNetworkErrorCallback?.Invoke(endPoint, socketError);
            }

            public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
            {

            } 

            public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
            {
#if NET_DEBUG
                UnityEngine.Debug.Log($"OnNetworkReceive: EndPoint:{peer.EndPoint} data size: {reader.UserDataSize}");
#endif
                if (reader.TryGetBytesWithLength(out byte[] data))
                {
                    OnNetworkReceiveCallback?.Invoke(peer.EndPoint, data);
                }
#if NET_DEBUG
                else
                {
                    UnityEngine.Debug.LogError($"TryGetBytesWithLength failed (EndPoint: {peer.EndPoint})");
                }
#endif
            }

            public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
            {

            }

            public void OnPeerConnected(NetPeer peer)
            {
#if NET_DEBUG
                UnityEngine.Debug.Log($"Open connection: {peer.EndPoint}");
#endif
                if (!m_connectedPeer.ContainsKey(peer.EndPoint))
                {
                    m_connectedPeer[peer.EndPoint] = peer;
                }
                OnConnectionCallback?.Invoke(peer.EndPoint);
            }

            public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
            {
#if NET_DEBUG
                UnityEngine.Debug.Log($"Connection closed: EndPoint:{peer.EndPoint} Reason:{disconnectInfo.Reason}");
#endif
                if (m_connectedPeer.ContainsKey(peer.EndPoint))
                {
                    m_connectedPeer.Remove(peer.EndPoint);
                }
                OnDisconnectionCallback?.Invoke(peer.EndPoint);
            }
        }
    }
    
}