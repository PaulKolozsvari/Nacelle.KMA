using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class PreScreeningDataPersonName
    {
        [JsonProperty("first")]
        public string First { get; set; }

        [JsonProperty("last")]
        public string Last { get; set; }
    }
}
