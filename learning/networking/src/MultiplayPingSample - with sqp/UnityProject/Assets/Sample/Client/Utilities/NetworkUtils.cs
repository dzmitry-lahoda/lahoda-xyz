using System;
using Unity.Networking.Transport;
using UnityEngine;

namespace MultiplayPingSample.Client
{
    public class NetworkUtils
    {
        const ushort k_DefaultServerPortToPing = 9000;

        /// <summary>
        ///     Parse a string of the format [IP Address]:[Port] into a NetworkEndPoint
        /// </summary>
        /// <param name="endpointToParse"></param>
        ///     A string of the format [IP Address]:[Port]
        /// <param name="serverEndPoint"></param>
        ///     The resulting NetworkEndPoint
        /// <param name="useDefaultsIfParsingFails"></param>
        ///     FALSE if strict parsing is desired; TRUE if parsing should fall back to defaults (loopback, default port) on failure
        /// <returns>
        ///     TRUE if parsing was successful, FALSE if not
        /// </returns>
        public static bool TryParseEndpoint(string endpointToParse, out NetworkEndPoint serverEndPoint, bool useDefaultsIfParsingFails = false)
        {
            // Start with a default IPv4 endpoint
            serverEndPoint = NetworkEndPoint.LoopbackIpv4;
            serverEndPoint.Port = k_DefaultServerPortToPing;

            if (string.IsNullOrEmpty(endpointToParse))
                return false;

            // Fix common formatting issues
            var address = "loopback";
            var customIp = endpointToParse.Trim().Replace(" ", "");
            var port = k_DefaultServerPortToPing;

            // Attempt to parse server endpoint string into a NetworkEndPoint
            var usedDefaultIp = true;
            var usedDefaultPort = true;

            if (!string.IsNullOrEmpty(customIp))
            {
                var endpoint = customIp.Split(':');

                switch (endpoint.Length)
                {
                    case 1:
                        // Try to parse IP, but use default port
                        usedDefaultIp = false;
                        break;

                    case 2:
                        // Try to parse IP
                        usedDefaultIp = false;
                        // Try to parse port
                        usedDefaultPort = !ushort.TryParse(endpoint[1].Trim(), out port);
                        break;
                }

                // Try to parse the first element into an IP address
                if (!usedDefaultIp && !NetworkEndPoint.TryParse(endpoint[0], port, out serverEndPoint))
                    usedDefaultIp = true;
                else
                    address = endpoint[0];
            }

            // If we're not allowed to fall back to defaults and we need to, error
            if (!useDefaultsIfParsingFails && (usedDefaultIp || usedDefaultPort))
            {
                Debug.LogError("Could not fully parse endpoint: " + customIp);
                return false;
            }

            return true;
        }
    }
}
