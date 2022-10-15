using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class Weight
    {
        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }
    }
}
