using System;
using System.Collections.Generic;
using System.Threading;
using Unity.Networking.QoS;
using Unity.Ucg.MmConnector;
using UnityEngine;

namespace MultiplayPingSample.Client
{
    // GUI
    public partial class GuiPingClientBehaviour : MonoBehaviour
    {
        static readonly Vector2 k_NativeSize = new Vector2(400, 425);

        void ScaleUI()
        {
            var scaleFactorX = Screen.width / k_NativeSize.x;
            var scaleFactorY = Screen.height / k_NativeSize.y;
            var scaleFactor = scaleFactorX < scaleFactorY ? scaleFactorX : scaleFactorY;
            var scale = new Vector3(scaleFactor, scaleFactor, 1.0f);
            GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, scale);
        }

        void OnGUI()
        {
            ScaleUI();

            GUILayout.BeginArea(new Rect(10,10, k_NativeSize.x - 20, k_NativeSize.y - 20));
            GUILayout.Label($"MULTIPLAY PING SAMPLE");

            switch (m_State)
            {
                case State.None:
                    Debug.LogError("State: None is invalid");
                    break;

                case State.Idle:
                    ShowPingMainUI();
                    ShowStatsUI();
                    break;

                case State.QosDiscovery:
                    ShowQosDiscoveryUI();
                    break;

                case State.QosPing:
                    ShowQosPingUI();
                    break;

                case State.Matchmaking:
                    ShowMatchmakingInProgressUI();
                    break;

                case State.ServerPing:
                    ShowPingCancelUI();
                    ShowStatsUI();
                    break;
            }

            GUILayout.EndArea();
        }

        void ShowQosPingUI()
        {
            GUILayout.Label($"Pinging Qos Servers for Fleet ID {FleetId}...");

            if (GUILayout.Button("Cancel"))
                TransitionToIdle();
        }

        void ShowQosDiscoveryUI()
        {
            GUILayout.Label($"Searching for Qos Servers for Fleet ID {FleetId}...");

            if (GUILayout.Button("Cancel"))
                TransitionToIdle();
        }

        void ShowPingMainUI()
        {
            if (GUILayout.Button("Start pinging server"))
                StartNewPingClient();

            if (GUILayout.Button("Terminate server"))
                TerminateRemoteServer();

            UseQosForMatchmaking = GUILayout.Toggle(UseQosForMatchmaking, "Use Qos in Matchmaking");

            if (GUILayout.Button("Use Matchmaking to find a server"))
                StartMatchmaking();

            GUILayout.Label("Matchmaking service url (empty = no matchmaking)");
            MatchmakingServiceUrl = GUILayout.TextField(MatchmakingServiceUrl);

            GUILayout.Label("Fleet ID (for QoS)");
            FleetId = GUILayout.TextField(FleetId);

            GUILayout.Label("Ping Server Endpoint:");
            m_PingServerEndpoint = GUILayout.TextField(m_PingServerEndpoint);
        }

        void ShowPingCancelUI()
        {
            GUILayout.Label("Pinging " + m_PingServerEndpoint);

            if (GUILayout.Button("Stop ping"))
                ShutdownPingClient();

            if (GUILayout.Button("Terminate connected server"))
                TerminateRemoteServer();
        }

        void ShowMatchmakingInProgressUI()
        {
            GUILayout.Label("Matchmaking");
            GUILayout.Label("Finding a server...");

            if (GUILayout.Button("Cancel"))
                CancelMatchmaking();
        }

        void ShowStatsUI()
        {
            GUILayout.Label($"\n\nFPS: {1.0f / Time.smoothDeltaTime:F}");
            GUILayout.Label(m_UdpPing?.GetStats() ?? "No Ping Stats Available");
            GUILayout.Label("Matchmaking state: " + m_MatchmakerState);
        }
    }

    // Non-GUI
    public partial class GuiPingClientBehaviour : MonoBehaviour
    {
        // Inspector variables & defaults
        public string FleetId = "";
        CancellationTokenSource m_MatchCts;
        string m_MatchmakerState;
        MatchmakingWrapper m_Matchmaking;
        string m_PingServerEndpoint;
        QosDiscoveryAsyncWrapper m_QosDiscovery;
        QosPingAsyncWrapper m_QosPing;
        IList<QosResultMultiplay> m_QosResults;
        QosServer[] m_QosServers;
        State m_State;
        UdpPingWrapper m_UdpPing;
        public string MatchmakingServiceUrl = "";
        public uint MatchmakingTimeoutMs = 0;
        public string PingServerEndpoint = "127.0.0.1:9000";
        public bool UseQosForMatchmaking = true;

        void Start()
        {
            ShutdownIfHeadless();
            m_PingServerEndpoint = PingServerEndpoint;
            m_State = State.Idle;
        }

        void TransitionToIdle()
        {
            CancelMatchmaking();
            m_State = State.Idle;
        }

        void ShutdownIfHeadless()
        {
            if (CommandLine.HasArgument("-nographics")
                || CommandLine.HasArgument("-batchmode"))
                Destroy(this);
        }

        void Update()
        {
            m_QosPing?.Update();
            m_Matchmaking?.Update();
            m_MatchmakerState = m_Matchmaking?.GetState() ?? m_MatchmakerState;
            m_UdpPing?.Update();
        }

        void StartQosMatchmaking(CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(FleetId))
            {
                Debug.LogError("Qos Discovery Failed - Fleet ID was missing or invalid");
                return;
            }

            // Start with Qos Discovery
            m_State = State.QosDiscovery;
            m_QosDiscovery = new QosDiscoveryAsyncWrapper();

            m_QosDiscovery.Start(FleetId, 0, servers =>
            {
                if (token.IsCancellationRequested)
                {
                    Debug.Log("Qos Discovery Cancelled");
                    m_State = State.Idle;
                    return;
                }

                m_QosServers = servers;

                if (m_QosServers == null || m_QosServers.Length == 0)
                {
                    Debug.LogError($"Qos Discovery Failed - No servers found for {FleetId}");
                    m_State = State.Idle;
                    return;
                }

                // Move on to Qos Ping
                StartQosPing(token);
            });
        }

        void StartQosPing(CancellationToken token = default)
        {
            m_State = State.QosPing;
            m_QosPing = new QosPingAsyncWrapper(m_QosServers);

            m_QosPing.Start(qosResults =>
            {
                if (token.IsCancellationRequested)
                {
                    Debug.Log("Qos Ping Cancelled");
                    m_State = State.Idle;
                    return;
                }

                if (qosResults == null || qosResults.Count == 0)
                {
                    Debug.LogError($"Qos Pinging Failed - No valid results found for {FleetId}");
                    m_State = State.Idle;
                    return;
                }

                m_QosResults = qosResults;
                QosConnector.Instance.RegisterProvider(() => m_QosResults);

                m_QosPing.Dispose();

                // Move on to start matchmaking
                SendMatchRequest(token);
            });
        }

        void SendMatchRequest(CancellationToken token = default)
        {
            m_State = State.Matchmaking;

            // Merge cancellation token w/ timeout
            if (MatchmakingTimeoutMs > 0)
            {
                var timeoutCts = new CancellationTokenSource((int)MatchmakingTimeoutMs);
                m_MatchCts = CancellationTokenSource.CreateLinkedTokenSource(token, timeoutCts.Token);
            }

            m_Matchmaking = new MatchmakingWrapper(MatchmakingServiceUrl);

            m_Matchmaking.Start(serverEndpoint =>
            {
                if (string.IsNullOrEmpty(serverEndpoint))
                {
                    Debug.LogError("Matchmaking Failed - No server connection information returned");
                    m_State = State.Idle;
                    return;
                }

                // Success!
                m_PingServerEndpoint = serverEndpoint;
                m_State = State.Idle;
            }, token);
        }

        void OnDestroy()
        {
            CancelMatchmaking();
        }

        void StartNewPingClient()
        {
            if (string.IsNullOrEmpty(m_PingServerEndpoint))
            {
                Debug.LogWarning("Cannot start pinging - No ping server endpoint was entered");
                return;
            }

            if (m_UdpPing != null)
            {
                Debug.LogWarning("Cannot start pinging - Pinging already in progress");
                return;
            }

            Debug.Log("Starting pinging...");

            try
            {
                m_UdpPing = new UdpPingWrapper();
                m_UdpPing.Start(m_PingServerEndpoint);
                m_State = State.ServerPing;
            }
            catch (Exception e)
            {
                Debug.LogError("Cannot start pinging due to exception - " + e.Message);
                m_UdpPing?.Dispose();
                m_UdpPing = null;
            }
        }

        void ShutdownPingClient()
        {
            if (m_UdpPing == null)
            {
                Debug.LogWarning("Cannot shut down ping client - no pinging in progress");
                return;
            }

            Debug.Log("Shutting down ping client...");
            m_UdpPing.Dispose();
            m_UdpPing = null;
            m_State = State.Idle;
        }

        void TerminateRemoteServer()
        {
            if (m_UdpPing != null)
            {
                m_UdpPing.TryTerminateRemoteServer();
                m_UdpPing.Dispose();
                m_UdpPing = null;
            }
            else
            {
                UdpPingWrapper.TryTerminateRemoteServer(m_PingServerEndpoint);
            }

            m_State = State.Idle;
        }

        void StartMatchmaking()
        {
            if (string.IsNullOrEmpty(MatchmakingServiceUrl))
            {
                Debug.LogWarning("Cannot start matchmaking - No matchmaker URL was entered");
                return;
            }

            if (m_Matchmaking != null && !m_Matchmaking.IsDone)
            {
                Debug.LogWarning("Cannot start new matchmaking request - matchmaking is already in progress");
                return;
            }

            // Reset cancellation token
            m_MatchCts = new CancellationTokenSource();

            // Clear existing qos results
            QosConnector.Instance.Reset();

            if (UseQosForMatchmaking)
                StartQosMatchmaking(m_MatchCts.Token);
            else
                SendMatchRequest(m_MatchCts.Token);
        }

        void CancelMatchmaking()
        {
            if (m_Matchmaking != null && !m_Matchmaking.IsDone)
            {
                m_MatchCts.Cancel();
                Debug.Log("Cancelling matchmaking...");
            }
        }

        enum State
        {
            None,
            Idle,
            QosDiscovery,
            QosPing,
            Matchmaking,
            ServerPing
        }
    }
}
