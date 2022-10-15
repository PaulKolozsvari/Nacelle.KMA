using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class BaggageAllowanceDefinition
    {
        [JsonProperty("allowanceSource")]
        public string AllowanceSource { get; set; }

        [JsonProperty("quantity")]
        public long Quantity { get; set; }
    }
}
