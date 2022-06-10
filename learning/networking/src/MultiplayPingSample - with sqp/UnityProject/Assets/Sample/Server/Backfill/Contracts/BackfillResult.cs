using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DefaultNamespace {
    public class BackfillResult
    {
        /// <summary>
        /// The tickets that were assigned to match created by the backfill function.
        /// </summary>
        public List<Ticket> AssignedTickets { get; set; }

        /// <summary>
        /// Custom information passed to the ticket assignments.
        /// </summary>
        public JObject AssignmentProperties { get; set; }

        /// <summary>
        /// If not null, will contain a description of error.
        /// </summary>
        public string Error { get; set; }
    }
}
