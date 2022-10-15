using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class PassengerSegment
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("passengerFlight")]
        public List<PassengerFlight> PassengerFlights { get; set; }

        [JsonProperty("segmentRef")]
        public string SegmentRef { get; set; }

        [JsonProperty("airExtra", NullValueHandling = NullValueHandling.Ignore)]
        public List<AirExtra> AirExtra { get; set; }
    }
}
