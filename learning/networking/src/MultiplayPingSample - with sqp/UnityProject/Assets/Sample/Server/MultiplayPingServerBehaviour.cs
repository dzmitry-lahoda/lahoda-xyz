using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace MultiplayPingSample.Server
{
    // Simple monobehavior driver for the underlying MultiplayPingServer
    //  Also exposes configuration data in inspector
    public class MultiplayPingServerBehaviour : MonoBehaviour
    {
        // The underlying server this class is controlling
        MultiplayPingServer m_Server;

        [Tooltip("Server configuration.  Properties set on the GameObject will be overridden by commandline arguments.")]
        public GameServerConfig.Config ServerConfig;

        void Start()
        {
#if UNITY_EDITOR
            // Ensure that the server is shutdown properly when stopped in the editor
            EditorApplication.playModeStateChanged += CleanUpOnExitingPlayMode;
#endif

            var version = $"PingSample_{Application.buildGUID}_{Application.unityVersion}";
            ServerConfig.Info.BuildId = version;

            m_Server = new MultiplayPingServer(ServerConfig);
        }

        void OnDestroy()
        {
            StopAllCoroutines();
            m_Server?.Dispose();
            m_Server = null;

#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= CleanUpOnExitingPlayMode;
#endif
        }

        void Update()
        {
            // Update as fast as our framerate
            m_Server?.Update();
        }

#if UNITY_EDITOR
        void CleanUpOnExitingPlayMode(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
                OnDestroy();
        }
#endif
    }
}
