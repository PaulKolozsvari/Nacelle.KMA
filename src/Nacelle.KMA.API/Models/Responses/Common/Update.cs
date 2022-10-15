using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class Update
    {
        [JsonProperty("context")]
        public string Context { get; set; }

        [JsonProperty("operation")]
        public string Operation { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
