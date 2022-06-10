using System;
using System.IO;
using UnityEngine;

namespace MultiplayPingSample.Server
{
    [Serializable]
    public class MultiplayConfig
    {
        public string SessionId;
        public string SessionAuth;
        public string ConnectionIP;
        public string ConnectionPort;

        /// <summary>
        /// Loads file from disk, throws if there is any IO/existence/access/formatting error.
        /// </summary>
        /// <exception cref="T:System.Exception">Thrown if there is any io error (file not exists/ path too long, cannot access</exception>
        public static MultiplayConfig ReadConfigFile(string configFilePath)
        {
            using (var file = new FileStream(configFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
            using (var reader = new StreamReader(file))
            {
                return  JsonUtility.FromJson<MultiplayConfig>(reader.ReadToEnd());
            }
        }
    }
}
