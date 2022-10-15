using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace Nacelle.KMA.API.Models.Responses
{
    public class AirportResponse
    {
        public List<Airport> Airports { get; set; }
    }

    public class Airport
    {
        [JsonProperty("IATA_CODE")]
        public string Code { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("CountrCode")]
        public string CountrCode { get; set; }

        [JsonProperty("CountryName")]
        public string CountryName { get; set; }
    }
}
