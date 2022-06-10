using System;
using UnityEngine;

namespace MultiplayPingSample.Client
{
    // A class that pings if it can
    public class UdpPingWrapper : IDisposable
    {
        UdpPingClient m_PingClient;
        PingStats m_Stats;

        public void Dispose()
        {
            if (m_PingClient != null)
            {
                m_PingClient.Dispose();
                m_PingClient = null;
                m_Stats?.StopMeasuring();
                m_Stats = null;
            }
        }

        // Update is called once per frame
        public void Update()
        {
            m_PingClient?.RunPingGenerator();
        }

        public string GetStats()
        {
            return "PING STATS:" +
                $"\nPings sent:\t {m_Stats?.TotalPings ?? 0}" +
                $"\nLatest ping:\t {m_Stats?.LastPing ?? 0} ms" +
                $"\nAverage ping:\t {m_Stats?.TotalAverage ?? 0:F} ms" +
                $"\n50-ping Average:\t {m_Stats?.GetRollingAverage() ?? 0:F} ms" +
                $"\nBest Ping: \t {m_Stats?.BestPing ?? 0} ms" +
                $"\nWorst Ping:\t {m_Stats?.WorstPing ?? 0} ms" +
                $"\nPings per second:\t {m_Stats?.PingsPerSecond() ?? 0:F}";
        }

        public void Start(string endpoint)
        {
            if (m_PingClient != null)
                throw new InvalidOperationException($"{nameof(Start)} cannot be called after pinging has already started");

            if (!NetworkUtils.TryParseEndpoint(endpoint, out var serverEndPoint))
                throw new ArgumentException("Could not parse endpoint");

            m_PingClient = new UdpPingClient(serverEndPoint);
            m_Stats = m_PingClient.Stats;
        }

        // Try to terminate the current server, then clean up the instance
        public bool TryTerminateRemoteServer(bool dispose = false)
        {
            if (TryTerminateRemoteServer(m_PingClient))
            {
                m_PingClient = null;

                if (dispose)
                    Dispose();

                return true;
            }

            return false;
        }

        public static bool TryTerminateRemoteServer(string endpoint)
        {
            return TryGetNewPingClient(endpoint, out var client) && TryTerminateRemoteServer(client);
        }

        public static bool TryTerminateRemoteServer(UdpPingClient client)
        {
            if (client == null)
                return false;

            client.ShutdownRemoteServer();
            client.Dispose();

            return true;
        }

        public static bool TryGetNewPingClient(string endpoint, out UdpPingClient newClient)
        {
            newClient = null;

            if (!NetworkUtils.TryParseEndpoint(endpoint, out var serverEndPoint))
            {
                Debug.LogError("Could not parse endpoint");
                return false;
            }

            try
            {
                newClient = new UdpPingClient(serverEndPoint);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Could not create new {nameof(UdpPingClient)}: {e.Message}");
                return false;
            }
        }
    }
}
