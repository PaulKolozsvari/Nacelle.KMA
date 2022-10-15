using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace Nacelle.KMA.API.Models.Requests
{
    public class WeatherRequest
    {
        [JsonProperty("airportIATACode")]
        public string AirportCode { get; set; }
    }
}
