using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class TypeClass
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
