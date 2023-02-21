using System;
using UnityEditor;
using UnityEngine;

namespace MultiplayPingSample.Client
{
    public static class PingUtilities
    {
        public static void EndProgram(int exitCode = 0)
        {
            Debug.Log("Shutting down Ping Client");

#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit(exitCode);
#endif
        }
    }
}
