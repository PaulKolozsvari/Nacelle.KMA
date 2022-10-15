using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class SegmentStatus
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
