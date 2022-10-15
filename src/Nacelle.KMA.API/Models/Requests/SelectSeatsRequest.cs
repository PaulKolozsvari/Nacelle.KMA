using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Requests
{
    public class SelectSeatsRequest
    {
        [JsonProperty("returnSession")]
        public bool ReturnSession { get; set; }

        [JsonProperty("seatNumber")]
        public string SeatNumber { get; set; }

        [JsonProperty("passengerFlightId")]
        public string PassengerFlightId { get; set; }
    }
}
