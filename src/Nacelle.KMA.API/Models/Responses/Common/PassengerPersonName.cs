using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class PassengerPersonName
    {
        [JsonProperty("first")]
        public string First { get; set; }

        [JsonProperty("last")]
        public string Last { get; set; }

        [JsonProperty("raw")]
        public string Raw { get; set; }
    }
}
