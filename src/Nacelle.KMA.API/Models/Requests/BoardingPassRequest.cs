using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Requests
{
    public class BoardingPassRequest
    {
        [JsonProperty("returnSession")]
        public bool ReturnSession { get; set; }

        [JsonProperty("passengerFlightIds")]
        public List<string> PassengerFlightIds { get; set; }

        [JsonProperty("outputFormat")]
        public string OutputFormat { get; set; }
    }
}
