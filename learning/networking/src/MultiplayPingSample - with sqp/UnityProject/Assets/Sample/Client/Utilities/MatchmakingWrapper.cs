using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Ucg.MmConnector;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Ucg.Matchmaking;

namespace MultiplayPingSample.Client
{
    public class MatchmakingWrapper : IDisposable
    {
        string m_AssignmentText;
        bool m_CancellationRequested;
        MatchmakingRequest m_MatchmakingRequest;
        string m_MatchmakingServiceUrl;

        public MatchmakingWrapper(string matchmakingServiceUrl)
        {
            m_MatchmakingServiceUrl = matchmakingServiceUrl;
        }

        public bool IsDone { get; private set; }
        public string Connection { get; private set; }
        public Action<string> Completed { get; set; }
        public CancellationToken CancellationToken { get; set; }

        public void Dispose()
        {
            m_MatchmakingRequest?.Dispose();
        }

        public string GetState()
        {
            var errorStringToAppend = string.IsNullOrEmpty(m_MatchmakingRequest?.ErrorString) ? "" : $": {m_MatchmakingRequest.ErrorString}";
            var assignmentTextToAppend = string.IsNullOrEmpty(m_AssignmentText) ? "" : $": {m_AssignmentText}";
            return (m_MatchmakingRequest?.State.ToString() ?? "") + errorStringToAppend + assignmentTextToAppend;
        }

        public void Start(Action<string> onCompletion = null, CancellationToken token = default)
        {
            if (m_MatchmakingRequest != null && !m_MatchmakingRequest.IsDone)
                throw new InvalidOperationException($"Cannot call {nameof(Start)} twice on the same {nameof(MatchmakingWrapper)}; it represents a single stateful operation.");

            Completed = onCompletion;
            CancellationToken = token;

            m_AssignmentText = null;

            // Create a new ticket (our custom data) to be passed in with our match request
            var ticket = new CreateTicketRequest
            {
                Attributes = BuildTicketAttributes(),
                Properties = BuildTicketProperties()
            };

            // Create a new match request
            m_MatchmakingRequest = new MatchmakingRequest(m_MatchmakingServiceUrl, ticket)
            {
                // Pass in our own non-default network settings (optional)
                GetTicketPollIntervalSeconds = 1.0f
            };

            m_MatchmakingRequest.Completed += OnMatchmakingComplete;
            m_MatchmakingRequest.SendRequest();
        }

        public IEnumerator StartEnumerator(Action<string> onCompletion = null, CancellationToken token = default)
        {
            try
            {
                Start(onCompletion, token);

                // Update until matchmaking is completed and has fired the completion handler
                while (!IsDone)
                {
                    Update();
                    yield return null;
                }
            }
            finally
            {
                m_MatchmakingRequest?.Dispose();
            }
        }

        void OnMatchmakingComplete(object sender, MatchmakingRequestCompletionArgs e)
        {
            // An Assignment object holds information about the match that your ticket was assigned to
            //  Getting an Assignment doesn't mean that matchmaking was "successful" - it just means that a match was assigned by the matchmaker
            //  Depending on how your match functions are configured, you can assign tickets to custom error states like "timed out"
            //  Assignments can also hold errors if there was an internal service problem
            var assignment = e.Assignment;

            // Handle completion
            Connection = assignment?.Connection;
            var connectionText = string.IsNullOrEmpty(Connection) ? null : $"\nConnection: {Connection}";
            var propertiesText = string.IsNullOrEmpty(assignment?.Properties) ? null : $"\nProperties: {assignment.Properties}";

            switch (e.State)
            {
                // This state represents success - assignment was returned with no error and a valid connection
                case MatchmakingRequestState.AssignmentReceived:
                    m_AssignmentText = connectionText ?? propertiesText;
                    Debug.Log($"Matchmaking completed:{connectionText}{propertiesText}");
                    break;

                // Failure case - the ticket was assigned to a match, but the assignment contained an error message
                case MatchmakingRequestState.ErrorAssignment:
                    m_AssignmentText = assignment?.Error;
                    Debug.LogError("Matchmaking assignment error: " + m_AssignmentText);
                    break;

                // Failure case - matchmaking failed for some reason (ex: bad request, networking issues, etc.)
                case MatchmakingRequestState.ErrorRequest:
                    Debug.LogError("Matchmaking failed: " + e.Error);
                    CleanUpTicket(e.TicketId);
                    break;

                // Manual cancellation case
                case MatchmakingRequestState.Canceled:
                    Debug.Log("Matchmaking Canceled");
                    break;

                // Unhandled cases
                default:
                    Debug.LogError("Unhandled matchmaking state: " + e.State);
                    CleanUpTicket(e.TicketId);
                    break;
            }

            IsDone = true;
            m_MatchmakingRequest.Dispose();
            Completed?.Invoke(Connection);
        }

        public void Update()
        {
            if (m_MatchmakingRequest == null || m_MatchmakingRequest.IsDone)
                return;

            // Handle cancellation
            if (CancellationToken.IsCancellationRequested && !m_CancellationRequested)
            {
                m_MatchmakingRequest.CancelRequest();
                m_CancellationRequested = true;
            }

            // Tick the matchmaker
            m_MatchmakingRequest?.Update();
        }

        // Immediately cancel, bypassing cancellation token use
        public void Cancel()
        {
            if (m_MatchmakingRequest == null || m_CancellationRequested)
            {
                Debug.LogWarning("Cannot cancel matchmaking because it's not in a cancellable state");
                return;
            }

            m_CancellationRequested = true;
            m_MatchmakingRequest.CancelRequest();
        }

        // Best practice: try to delete your tickets if you end up in a bad state and don't plan to retry
        // This sends a fire-and-forget ticket deletion request
        void CleanUpTicket(string ticketId)
        {
            if (string.IsNullOrEmpty(ticketId))
                return;

            var url = MatchmakingClient.BuildMatchmakingServiceUrl(m_MatchmakingServiceUrl);
            var delOp = MatchmakingClient.DeleteTicketAsync(url, ticketId);

            delOp.completed += asyncOp =>
            {
                var request = ((UnityWebRequestAsyncOperation)asyncOp).webRequest;
                request.Dispose();
            };
        }

        // Create a list of attributes to append to a matchmaking ticket
        static Dictionary<string, double> BuildTicketAttributes()
        {
            // Attributes are a collection of named values which can be indexed for fast filtering inside a match function
            return new Dictionary<string, double>
            {
                { "skill", 1234 },
                { "mode", 1 },
                { "playercount", 1 },
                { "smoke-test", 1 }
            };
        }

        // Create a list of custom properties to append to a matchmaking ticket
        static Dictionary<string, string> BuildTicketProperties()
        {
            // Ticket properties are a collection of non-indexed objects which you can deserialize and access inside a match function
            // You can use properties to attach any arbitrary data you want to a ticket
            var properties = new Dictionary<string, string>();

            // In this example, we attach the data from a custom "CustomTicketProperties" object to the ticket
            var customProperties = new PartyInfo { id = Guid.NewGuid() };
            properties.Add(customProperties.GetType().Name.ToLower(), JsonUtility.ToJson(customProperties));

            return properties;
        }

        // Custom properties to attach to a ticket
        //  This also shows an example of how to set up a custom serializer
        //  and deserializer for use with JsonUtility
        [Serializable]
        class PartyInfo : ISerializationCallbackReceiver
        {
            [SerializeField]
            string[] AvoidParties;
            [SerializeField]
            string Id;

            public Guid id { get; set; }

            public HashSet<Guid> avoidParties { get; set; } = new HashSet<Guid>();

            // JsonUtility can't encode Guids or sets of Guids directly
            // To encode them, we can convert Guids to strings and the HashSet to a list
            public void OnBeforeSerialize()
            {
                Id = id.ToString();
                AvoidParties = new string[avoidParties.Count];

                var i = 0;

                foreach (var foo in avoidParties)
                {
                    AvoidParties[i] = foo.ToString();
                    i++;
                }
            }

            public void OnAfterDeserialize()
            {
                id = default;
                avoidParties = new HashSet<Guid>();

                if (Guid.TryParse(Id, out var idAsGuid))
                    id = idAsGuid;

                foreach (var foo in AvoidParties)
                    if (Guid.TryParse(foo, out var fooGuid))
                        avoidParties.Add(fooGuid);
            }
        }
    }
}
