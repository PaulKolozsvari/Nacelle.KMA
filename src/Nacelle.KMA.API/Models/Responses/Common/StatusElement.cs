using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class StatusElement
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
