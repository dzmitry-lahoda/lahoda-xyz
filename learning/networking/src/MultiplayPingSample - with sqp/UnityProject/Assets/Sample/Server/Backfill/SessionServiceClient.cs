using System;
using DefaultNamespace;
using MultiplayPingSample.Server.Utilities;

namespace MultiplayPingSample.Server {
    public class SessionServiceClient : HttpClientBase
    {
        public SessionServiceClient(string bearerToken) : base("https://server-session.multiplay.com/v1/session", bearerToken)
        {
        }

        public  AsyncResult<MatchProperties> GetSessionDataAsync(string sessionId, int timeout = k_DefaultTimeoutSeconds)
        {
            return GetJsonAsync<MatchProperties>(sessionId, timeout);
        }
    }
}
