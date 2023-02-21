using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DefaultNamespace {
    public class BackfillRequest
    {
        /// <summary>
        /// Typically the "IP:Port" of the server making the backfill request; this will be assigned to all tickets.
        /// Required.
        /// </summary>
        public string Connection { get; set; }

        /// <summary>
        /// The matchfunction name and version to execute for the backfill request.
        /// Required.
        /// </summary>
        public MatchFunction Function { get; set; }

        /// <summary>
        /// Map of pool names to the filters in the pool.
        /// Optional.
        /// </summary>
        public Dictionary<string, List<Filter>> Pools { get; set; }

        /// <summary>
        /// Custom configuration object to be passed to match function.
        /// Optional.
        /// </summary>
        public JObject Config { get; set; }
    }
}
