using System;
using DefaultNamespace;
using MultiplayPingSample.Server.Utilities;
using UnityEngine;

namespace MultiplayPingSample.Server
{
    public class BackfillClient : HttpClientBase
    {
        public BackfillClient(string projectId, string bearerToken) : base(CreateUri(projectId), bearerToken)
        {
        }

        static string CreateUri(string projectId)
        {
            projectId = projectId ?? throw new ArgumentNullException(nameof(projectId));
            return $"https://cloud.connected.unity3d.com/{projectId}/matchmaking/api/v1/backfill";
        }

        public  AsyncResult<BackfillResult> PostBackfillRequestAsync(BackfillRequest request, int timeout = k_DefaultTimeoutSeconds)
        {
            return PostJsonAsync<BackfillResult>(request,string.Empty, timeout);
        }
    }

}
