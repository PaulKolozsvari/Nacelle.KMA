using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class AncillaryRules
    {
        [JsonProperty("commissionable")]
        public bool Commissionable { get; set; }

        [JsonProperty("exchangeable")]
        public bool Exchangeable { get; set; }

        [JsonProperty("feeApplicationMethod")]
        public Status FeeApplicationMethod { get; set; }

        [JsonProperty("interlineable")]
        public bool Interlineable { get; set; }

        [JsonProperty("paperTicketRequired")]
        public bool PaperTicketRequired { get; set; }

        [JsonProperty("refundable")]
        public bool Refundable { get; set; }
    }
}
