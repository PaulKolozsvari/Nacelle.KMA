using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class Eligibility
    {
        [JsonProperty("notEligible")]
        public bool NotEligible { get; set; }

        [JsonProperty("reason")]
        public List<Reason> Reason { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
