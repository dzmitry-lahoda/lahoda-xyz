using System;
using System.Collections;
using Unity.Networking.QoS;
using UnityEngine;

namespace MultiplayPingSample.Client
{
    public class QosDiscoveryAsyncWrapper
    {
        const int k_DefaultDiscoveryTimeoutSec = 10;
        QosDiscovery m_Discovery;
        Action<QosServer[]> m_OnCompletion;

        public QosServer[] Servers { get; private set; } = new QosServer[0];

        public bool IsDone { get; private set; }

        // Start a Qos Discovery query
        public void Start(string fleetId, int discoveryTimeoutSec = 0, Action<QosServer[]> onCompletion = null)
        {
            discoveryTimeoutSec = discoveryTimeoutSec > 0 ? discoveryTimeoutSec : k_DefaultDiscoveryTimeoutSec;

            m_OnCompletion = onCompletion;

            var fleet = fleetId?.Trim();

            if (string.IsNullOrEmpty(fleet))
                throw new ArgumentNullException(nameof(fleetId), $"{nameof(fleetId)} was null or empty; skipping discovery");

            m_Discovery = new QosDiscovery();

            m_Discovery.StartDiscovery(fleet, discoveryTimeoutSec, DiscoverySuccess, DiscoveryError);
        }

        // Start a Qos Discovery query and return an enumerator that can be used in a coroutine
        public IEnumerator StartEnumerator(string fleetId, int discoveryTimeoutSec = 0, Action<QosServer[]> onCompletion = null)
        {
            Start(fleetId, discoveryTimeoutSec, onCompletion);

            if (m_Discovery == null)
                yield break;

            while (!m_Discovery.IsDone)
                yield return null;
        }

        void DiscoverySuccess(QosServer[] servers)
        {
            Servers = servers ?? Servers;

            IsDone = true;
            m_OnCompletion?.Invoke(Servers);
        }

        void DiscoveryError(string error)
        {
            Debug.LogError(error);

            IsDone = true;
            m_OnCompletion?.Invoke(Servers);
        }
    }
}
