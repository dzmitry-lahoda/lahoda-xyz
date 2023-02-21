using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DefaultNamespace {
    public class MatchProperties
    {
        public BackendExpansion Expansion { get; set; }
        public List<Ticket> Tickets { get; set; }
        public JObject AssignmentProperties { get; set; }
        public string GeneratorName { get; set; }
        public string FunctionName { get; set; }
    }
}
