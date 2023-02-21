using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.Networking.QoS;
using Unity.Ucg.MmConnector;
using UnityEngine;

namespace MultiplayPingSample.Client
{
    public class QosPingAsyncWrapper : IDisposable
    {
        const string k_DefaultTitle = "MultiplayPingSample";
        const uint k_DefaultRequestsPerEndpoint = 10;
        const ulong k_DefaultTimeoutMs = 5000;
        const float k_DefaultWeightOfCurrentResult = 0.75f;
        const uint k_DefaultQosResultHistory = 5;
        Action<IList<QosResultMultiplay>> m_OnCompletion;
        QosJob m_QosJob;
        JobHandle m_QosJobHandle;
        QosServer[] m_QosServers;

        uint m_RequestsPerEndpoint = k_DefaultRequestsPerEndpoint;
        bool m_Started;
        QosStats m_Stats;
        ulong m_TimeoutMs = k_DefaultTimeoutMs;

        public QosPingAsyncWrapper(QosServer[] qosServers)
        {
            m_QosServers = qosServers;
        }

        // True when the the Qos Ping operation is completed
        public bool IsDone { get; private set; }

        // The results of the qos ping query (only available if qos ping has been successfully run)
        public IList<QosResultMultiplay> QosResults { get; private set; }

        public void Dispose()
        {
            if (m_QosJob.QosResults.IsCreated)
                m_QosJob.QosResults.Dispose();
        }

        // Start pinging the qos servers attached to this instance
        public void Start(Action<IList<QosResultMultiplay>> onCompletion = null, uint requestsPerEndpoint = 0, ulong timeoutMs = 0)
        {
            if (m_Started)
            {
                Debug.LogWarning($"Attempted to call {nameof(Start)} on a {nameof(QosPingAsyncWrapper)} that has already been started");
                return;
            }

            m_Started = true;
            m_OnCompletion = onCompletion;

            if (requestsPerEndpoint > 0)
                m_RequestsPerEndpoint = requestsPerEndpoint;

            if (timeoutMs > 0)
                m_TimeoutMs = timeoutMs;

            (m_QosJob, m_QosJobHandle) = SchedulePingJob(m_QosServers);
        }

        // Start pinging the qos servers attached to this instance and return a yield-able enumerator for use with coroutines
        public IEnumerator StartEnumerator(Action<IList<QosResultMultiplay>> onCompletion = null, uint requestsPerEndpoint = 0, ulong timeoutMs = 0)
        {
            if (m_Started)
            {
                Debug.LogWarning($"Attempted to call {nameof(StartEnumerator)} on a {nameof(QosPingAsyncWrapper)} that has already been started");
                yield break;
            }

            Start(onCompletion, requestsPerEndpoint, timeoutMs);

            // Wait for job to complete
            while (!m_QosJobHandle.IsCompleted)
                yield return null;

            HandleQosJobCompletion();
        }

        // Tick the qos ping state - Does not need to be called if using StartEnumerator with a coroutine
        public void Update()
        {
            if (!m_QosJobHandle.IsCompleted || IsDone)
                return;

            HandleQosJobCompletion();
        }

        void HandleQosJobCompletion()
        {
            if (IsDone)
                return;

            IsDone = true;

            // Complete job
            m_QosJobHandle.Complete();

            // Update stats and parse into results
            UpdateStatsWithJobResults(ref m_Stats, m_QosJob, m_QosServers);
            QosResults = GetQosResultsForServersFromStats(m_Stats, m_QosServers);

            // Clean up QosJob
            if (m_QosJob.QosResults.IsCreated)
                m_QosJob.QosResults.Dispose();

            // Invoke completion callback if registered
            m_OnCompletion?.Invoke(QosResults);
        }

        (QosJob, JobHandle) SchedulePingJob(IList<QosServer> qosServers)
        {
            // Create and schedule a new QoS job
            var qosJob = new QosJob(qosServers, k_DefaultTitle)
            {
                RequestsPerEndpoint = m_RequestsPerEndpoint,
                TimeoutMs = m_TimeoutMs
            };

            var qosJobHandle = qosJob.Schedule();

            JobHandle.ScheduleBatchedJobs();

            return (qosJob, qosJobHandle);
        }

        static IList<QosResultMultiplay> GetQosResultsForServersFromStats(QosStats qosStats, QosServer[] qosServers)
        {
            var results = new List<QosResultMultiplay>();

            if (qosServers != null && qosStats != null)
                foreach (var qs in qosServers)
                    if (qosStats.TryGetWeightedAverage(qs.ToString(), out var result))
                        results.Add(new QosResultMultiplay
                        {
                            Location = qs.locationid,
                            Region = qs.regionid,
                            Latency = result.LatencyMs,
                            PacketLoss = result.PacketLoss
                        });

            return results;
        }

        static void UpdateStatsWithJobResults(ref QosStats qosStats, QosJob qosJob, QosServer[] qosServers)
        {
            if (!qosJob.QosResults.IsCreated)
                return;

            var results = qosJob.QosResults.ToArray();

            if (results.Length == 0)
                return;

            // We've got stats to record, so create a new stats tracker if we don't have one yet
            qosStats = qosStats ?? new QosStats(k_DefaultQosResultHistory, k_DefaultWeightOfCurrentResult);

            // Add stats for each endpoint to the stats tracker
            for (var i = 0; i < results.Length; ++i)
            {
                var ipAndPort = qosServers[i].ToString();
                var r = results[i];
                qosStats.AddResult(ipAndPort, r);

                // Deal with flow control in results (must have gotten at least one response back)
                if (r.ResponsesReceived > 0 && r.FcType != FcType.None)
                {
                    qosServers[i].BackoffUntilUtc = GetBackoffUntilTime(r.FcUnits);
                    Debug.Log($"{ipAndPort}: Server applied flow control and will no longer respond until {qosServers[i].BackoffUntilUtc}.");
                }
            }
        }

        // Do not modify - Contract with server
        static DateTime GetBackoffUntilTime(byte fcUnits)
        {
            // 2 minutes for each unit, plus 30 seconds buffer
            return DateTime.UtcNow.AddMinutes(2 * fcUnits + 0.5f);
        }
    }
}
