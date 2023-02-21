using System;
using Newtonsoft.Json.Linq;

namespace DefaultNamespace {
    /// <summary>
    /// A first-class model for ticket assignments. Contains the "final" result of matchmaking for a ticket
    /// </summary>
    public class Assignment
    {
        /// <summary>
        /// Information about where to go next. E.G an ip:port, or an existing session locator
        /// </summary>
        public string Connection { get; set; }

        /// <summary>
        /// Matchmaking specific error information
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Custom properties for population by a director or match function
        /// </summary>
        public JObject Properties { get; set; }
    }
}
