using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class Agent
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("sign")]
        public string Sign { get; set; }
    }
}
