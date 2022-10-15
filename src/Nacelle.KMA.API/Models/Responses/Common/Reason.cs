using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class Reason
    {
        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
