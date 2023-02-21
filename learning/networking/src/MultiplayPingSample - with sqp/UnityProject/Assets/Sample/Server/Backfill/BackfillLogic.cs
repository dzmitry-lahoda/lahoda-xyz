using System;
using DefaultNamespace;
using MultiplayPingSample.Server.Utilities;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace MultiplayPingSample.Server
{
    public class BackfillLogic
    {
        MultiplayConfig m_MultiplayConfig;
        ServerSettings m_Settings;

        public BackfillLogic(MultiplayConfig multiplayConfig, ServerSettings settings)
        {
            m_Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            m_MultiplayConfig = multiplayConfig ?? throw new ArgumentNullException(nameof(multiplayConfig));
        }

        public void PerformBackfill()
        {
            if (string.IsNullOrEmpty(m_Settings?.ProjectId)) throw new ArgumentNullException(nameof(ServerSettings.ProjectId));
            if (string.IsNullOrEmpty(m_MultiplayConfig?.SessionId)) throw new ArgumentNullException(nameof(MultiplayConfig.SessionId));
            if (string.IsNullOrEmpty(m_MultiplayConfig?.ConnectionIP)) throw new ArgumentNullException(nameof(MultiplayConfig.ConnectionIP));
            if (string.IsNullOrEmpty(m_MultiplayConfig?.ConnectionPort)) throw new ArgumentNullException(nameof(MultiplayConfig.ConnectionPort));

            var sessionClient = new SessionServiceClient(m_MultiplayConfig.SessionAuth);
            var sessionResult = sessionClient.GetSessionDataAsync(m_MultiplayConfig.SessionId);
            sessionResult.exception += OnSessionResultException;
            sessionResult.completed += OnSessionDataRequestComplete;
        }

        void OnSessionResultException(Exception e)
        {
            Debug.LogError("Error fetching session data:\n " + e);
        }

        void OnSessionDataRequestComplete(MatchProperties matchProperties)
        {
            // TODO do something with result
            Debug.Log("Got back match properties: " + JObject.FromObject(matchProperties));

            // Note: here is where you would want to pass specific config from the MatchProperties object into the backfill request config
            // this sample creates the simple config required by the simple backfill function
            var client = new BackfillClient(m_Settings.ProjectId, m_MultiplayConfig.SessionAuth);

            var result = client.PostBackfillRequestAsync(new BackfillRequest
            {
                Config = JObject.FromObject(new SimpleBackfillConfig { PlayersNeeded = 1 }),
                Connection = $"{m_MultiplayConfig.ConnectionIP}:{m_MultiplayConfig.ConnectionPort}",
                Function = new MatchFunction { Name = "simplebackfill-function" },
                Pools = matchProperties.Expansion.Pools
            });

            result.exception += OnBackfillResultException;
            result.completed += OnBackfillRequestCompleted;
        }

        static void OnBackfillResultException(Exception e)
        {
            Debug.LogError("Error executing backfill request:\n " + e);
        }

        static void OnBackfillRequestCompleted(BackfillResult backfillResult)
        {
            Debug.Log("Backfill Response: " + JObject.FromObject(backfillResult));
        }
    }
}
