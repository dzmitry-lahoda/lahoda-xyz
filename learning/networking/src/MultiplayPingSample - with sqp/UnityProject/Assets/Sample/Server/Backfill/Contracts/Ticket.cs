using System;
using System.Collections.Generic;

namespace DefaultNamespace {
    /// <summary>
    /// Intended to be used as a data model abstraction for handling groups of players organized into indexable tickets
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// The identifier of the ticket tracked by clients
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// A string for allowing a backend to provide assignment information (TODO: Possibly a typed contract for Unity)
        /// </summary>
        public Assignment Assignment { get; set; }

        /// <summary>
        /// Range indexes
        /// </summary>
        public IDictionary<string, double> Attributes { get; set; }

        /// <summary>
        /// The milliseconds in unix utc representing when this ticket was created
        /// </summary>
        public long Created {get;set;}

        /// <summary>
        /// Custom data provided by the ticket creator. Keys must be lower case.
        /// </summary>
        public Dictionary<string, byte[]> Properties { get; set; }
    }
}