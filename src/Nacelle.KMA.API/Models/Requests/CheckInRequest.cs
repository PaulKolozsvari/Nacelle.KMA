using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Requests
{
    public class CheckInRequest
    {
        [JsonProperty("returnSession")]
        public bool ReturnSession { get; set; }

        [JsonProperty("passengerIds")]
        public List<string> PassengerIds { get; set; }

        [JsonProperty("outputFormat")]
        public string OutputFormat { get; set; }
    }
}
