using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class PassengerFlight
    {
        [JsonProperty("checkedIn")]
        public bool CheckedIn { get; set; }

        [JsonProperty("flightRefs")]
        public List<string> FlightRefs { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("seat", NullValueHandling = NullValueHandling.Ignore)]
        public TypeClass Seat { get; set; }

        [JsonProperty("boardingPass", NullValueHandling = NullValueHandling.Ignore)]
        public BoardingPass  BoardingPass { get; set; }
    }
}
