using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class CheckedInBaggage
    {
        [JsonProperty("baggageAllowanceDefinition")]
        public List<BaggageAllowanceDefinition> BaggageAllowanceDefinition { get; set; }
    }
}
