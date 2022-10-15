using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class SpecialServiceDescription
    {
        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
