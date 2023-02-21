using System;
using System.IO;
using MultiplayPingSample.Server.Utilities;
using Newtonsoft.Json;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace MultiplayPingSample.Server
{
    public class MultiplayPingServer : IDisposable
    {
        // Explicitly defined exit codes
        public enum ExitCode
        {
            Ok = 0,
            Error = 1
        }

        const string k_SettingsFileName = "settings.json";
        static readonly TimeSpan k_ConfigCheckInterval = TimeSpan.FromSeconds(1);
        string m_ConfigPath;
        bool m_Disposed;
        GameServerUdpPingHandler m_GameServerUdpPingHandler;
        bool m_Initialized;
        DateTime m_LastConfigCheck = DateTime.MinValue;
        MultiplayConfig m_MultiplayConfig;
        MultiplayServerStatusQueryHandler m_MultiplayServerStatusQueryHandler;
        GameServerConfig.Config m_ServerConfig;
        ServerSettings m_ServerSettings;

        public MultiplayPingServer(GameServerConfig.Config config = null)
        {
            // Initialize
            try
            {
                m_ServerConfig = GameServerConfig.GetConfig(config);

                // Update path to multiplay session data file
                m_ConfigPath = m_ServerConfig.MultiplayConfigPath;
                if (string.IsNullOrEmpty(m_ConfigPath))
                    throw new Exception("-config argument must be specified!");

                // Read server settings file
                var settingsFileText = File.ReadAllText(Path.Combine(Application.dataPath, "StreamingAssets", k_SettingsFileName));
                m_ServerSettings = JsonConvert.DeserializeObject<ServerSettings>(settingsFileText);
                if (m_ServerSettings == null || string.IsNullOrEmpty(m_ServerSettings.ProjectId))
                    throw new Exception("Project ID is invalid.  Did you set it in your StreamingAssets/settings.json file?");

                // Spin up Multiplay health check handler
                m_MultiplayServerStatusQueryHandler = new MultiplayServerStatusQueryHandler(
                    m_ServerConfig.IpAddress, m_ServerConfig.SqpPort);

                // Start handling ping client connections
                m_GameServerUdpPingHandler = new GameServerUdpPingHandler(
                    m_ServerConfig.IpAddress, m_ServerConfig.Info.Port, m_ServerConfig.Info.MaxPlayers);
            }
            catch (Exception e)
            {
                // Kill server if it can't be initialized
                Debug.LogError($"{nameof(MultiplayPingServer)} failed to initialize server: {e.Message}");
                ShutdownServer(ExitCode.Error);
            }
        }

        public void Dispose()
        {
            if (m_Disposed)
                return;

            m_MultiplayServerStatusQueryHandler?.Dispose();
            m_GameServerUdpPingHandler?.Dispose();

            m_Disposed = true;
        }

        ~MultiplayPingServer()
        {
            Dispose();
        }

        public void Update()
        {
            // Watch for session change
            CheckForSessionChange();

            // Update the UDP ping handler (responds to client pings)
            UpdateUdpPingServer();

            // Update Multiplay health check data
            UpdateSqp();
        }

        public void UpdateUdpPingServer()
        {
            // Respond to connected clients
            if (m_GameServerUdpPingHandler != null)
            {
                m_GameServerUdpPingHandler.Update();

                // Trigger shutdown if requested by remote command
                if (m_GameServerUdpPingHandler.ShouldShutdown)
                {
                    Debug.Log("Server received shutdown signal from a client; shutting down");
                    ShutdownServer(ExitCode.Ok);
                }

                // Update the activity check (while active) so the server is not auto-shutdown for idling
                if (m_GameServerUdpPingHandler.ConnectedClients > 0)
                    DedicatedServerConfig.UpdateLastActivity();
            }
        }

        public void UpdateSqp()
        {
            // Respond to server health checks
            if (m_MultiplayServerStatusQueryHandler != null)
            {
                // Update number of active players
                m_MultiplayServerStatusQueryHandler.Info.CurrentPlayers = m_GameServerUdpPingHandler?.ConnectedClients ?? 0;

                // Set other vars
                m_MultiplayServerStatusQueryHandler.Info.MaxPlayers = m_ServerConfig.Info.MaxPlayers;
                m_MultiplayServerStatusQueryHandler.Info.BuildId = m_ServerConfig.Info.BuildId;
                m_MultiplayServerStatusQueryHandler.Info.GameType = m_ServerConfig.Info.GameType;
                m_MultiplayServerStatusQueryHandler.Info.Map = m_ServerConfig.Info.Map;
                m_MultiplayServerStatusQueryHandler.Info.ServerName = m_ServerConfig.Info.ServerName;
                m_MultiplayServerStatusQueryHandler.Info.Port = m_ServerConfig.Info.Port;

                // Tick the update
                m_MultiplayServerStatusQueryHandler.Update();
            }
        }

        /// <summary>
        ///     When the session ID is updated by Multiplay, it indicates that a new match has begun. There will also be new match
        ///     data available via the session service endpoint.
        ///     If backfill is dependent on reading the previous match data, this is the first valid point in time to call
        ///     backfill.
        /// </summary>
        void CheckForSessionChange()
        {
            if (DateTime.Now - m_LastConfigCheck < k_ConfigCheckInterval)
                return;

            m_LastConfigCheck = DateTime.Now;

            if (!File.Exists(m_ConfigPath))
                return;

            var newConfig = MultiplayConfig.ReadConfigFile(m_ConfigPath);

            // Diff session/allocation ids to see if config has been updated:
            if (m_MultiplayConfig?.SessionId == newConfig?.SessionId || string.IsNullOrEmpty(newConfig?.SessionId))
                return; // Session ID hasn't changed, don't do anything

            Debug.Log($"Session Id Changed: Old Value '{m_MultiplayConfig?.SessionId ?? "null"}', new Value '{newConfig.SessionId}'");
            m_MultiplayConfig = newConfig;
            var backfillLogic = new BackfillLogic(m_MultiplayConfig, m_ServerSettings);
            backfillLogic.PerformBackfill();
        }

        // Turn off the server (application)
        public void ShutdownServer(ExitCode exitCode)
        {
            m_GameServerUdpPingHandler?.ShutDown();
            m_MultiplayServerStatusQueryHandler?.Dispose();

            Debug.Log("PingServer shutting down...");
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit((int)exitCode);
#endif
        }
    }
}
