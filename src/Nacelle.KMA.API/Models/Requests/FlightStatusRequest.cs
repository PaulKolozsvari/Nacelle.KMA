using Newtonsoft.Json;
using System;

namespace Nacelle.KMA.API.Models.Requests
{
    public class FlightStatusRequest
    {
        [JsonProperty("departureDate")]
        public DateTimeOffset DepartureDate { get; set; }

        [JsonProperty("departureCode")]
        public string DepartureCode { get; set; }

        [JsonProperty("flightNumber")]
        public string FlightNumber { get; set; }
    }
}
