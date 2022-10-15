using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class AirExtra
    {
        [JsonProperty("ancillary")]
        public Ancillary Ancillary { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("paymentStatus")]
        public PaymentStatus PaymentStatus { get; set; }

        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        [JsonProperty("syntheticIdentifier")]
        public string SyntheticIdentifier { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("weight", NullValueHandling = NullValueHandling.Ignore)]
        public Weight Weight { get; set; }

        [JsonProperty("specialServiceDescription", NullValueHandling = NullValueHandling.Ignore)]
        public SpecialServiceDescription SpecialServiceDescription { get; set; }
    }
}
