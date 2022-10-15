using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class Segment
    {
        [JsonProperty("baggageCheckInRules", NullValueHandling = NullValueHandling.Ignore)]
        public BaggageCheckInRules BaggageCheckInRules { get; set; }

        [JsonProperty("fareInfo")]
        public FareInfo FareInfo { get; set; }

        [JsonProperty("flightDetail")]
        public List<FlightDetail> FlightDetail { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("status")]
        public TypeClass Status { get; set; }

        [JsonProperty("eligibilities", NullValueHandling = NullValueHandling.Ignore)]
        public Eligibilities Eligibilities { get; set; }
    }
}
