using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPCombatApp
{
    // Drill item for interacting with the database
    class DrillItem
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "sets")]
        public int Sets { get; set; }

        [JsonProperty(PropertyName = "settime")]
        public int SetTime { get; set; }

        [JsonProperty(PropertyName = "style")]
        public string Style { get; set; }

        [JsonProperty(PropertyName = "use")]
        public string Use { get; set; }

    }
}
