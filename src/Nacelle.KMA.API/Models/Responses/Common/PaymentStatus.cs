using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class PaymentStatus
    {
        [JsonProperty("statusCode")]
        public string StatusCode { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
