using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class HomePrintedBagTagRestricted
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
