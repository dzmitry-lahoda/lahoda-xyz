using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DefaultNamespace {
    public class BackendExpansion
    {
        public TargetFunction Target { get; set; }
        public JObject Config { get; set; }
        public Dictionary<string, List<Filter>> Pools { get; set; }
    }
}
