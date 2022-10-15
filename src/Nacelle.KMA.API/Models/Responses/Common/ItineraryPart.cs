using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class ItineraryPart
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("segment")]
        public List<Segment> Segment { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
