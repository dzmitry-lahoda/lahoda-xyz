using System;
using UnityEngine;

namespace MultiplayPingSample.Client
{
    public class ClientWatcherBehaviour : MonoBehaviour
    {
        // Vars that can be set via the inspector
        public ushort ClientTargetFps = 60;

        // Start is called before the first frame update
        void Awake()
        {
            CommandLine.PrintArgsToLog();
            SetFps();
        }

        void Update()
        {
            ShutdownIfInvalid();
        }

        // Set client FPS
        // FPS currently has an impact on 
        void SetFps()
        {
            // Read FPS argument and override default / game object settings
            CommandLine.TryUpdateVariableWithArgValue(ref ClientTargetFps, "-fps");

            // Set FPS
            if (ClientTargetFps > 0)
            {
                Application.targetFrameRate = ClientTargetFps;
                Debug.Log($"Settings FPS to {ClientTargetFps}");
            }
            
            // If requested FPS is different from current screen resolution, disable VSync
            if (ClientTargetFps != Screen.currentResolution.refreshRate)
            {
                QualitySettings.vSyncCount = 0;
                Debug.Log($"Disabling VSYNC");
            }
        }

        // Shut down the client if in an invalid client state
        void ShutdownIfInvalid()
        {
            var headlessClient = gameObject.GetComponent<HeadlessPingClientBehaviour>();
            var guiClient = gameObject.GetComponent<GuiPingClientBehaviour>();

            if (headlessClient == null && guiClient == null)
            {
                Debug.LogError($"{nameof(HeadlessPingClientBehaviour)} and {nameof(GuiPingClientBehaviour)} couldn't be found, shutting down.");
                PingUtilities.EndProgram();
            }
        }
    }
}
