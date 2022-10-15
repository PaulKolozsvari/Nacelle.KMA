using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class BoardingPassElement
    {
        [JsonProperty("boardingPass")]
        public BoardingPass BoardingPass { get; set; }

        [JsonProperty("passengerFlightId")]
        public string PassengerFlightId { get; set; }

        [JsonIgnore]
        public bool HasQJump { get; set; }
    }
}
