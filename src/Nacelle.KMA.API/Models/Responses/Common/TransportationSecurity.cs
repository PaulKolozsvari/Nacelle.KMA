using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class TransportationSecurity
    {
        [JsonProperty("preScreeningData")]
        public PreScreeningData PreScreeningData { get; set; }
    }
}
