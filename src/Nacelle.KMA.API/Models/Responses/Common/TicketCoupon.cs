using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class TicketCoupon
    {
        [JsonProperty("baggageDisclosure")]
        public BaggageDisclosure BaggageDisclosure { get; set; }

        [JsonProperty("couponNumber")]
        public string CouponNumber { get; set; }

        [JsonProperty("fareBasisCode")]
        public string FareBasisCode { get; set; }

        [JsonProperty("segmentRef")]
        public string SegmentRef { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
