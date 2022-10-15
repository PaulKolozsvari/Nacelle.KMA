using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class Ancillary
    {
        [JsonProperty("airline")]
        public string Airline { get; set; }

        [JsonProperty("ancillaryRules")]
        public AncillaryRules AncillaryRules { get; set; }

        [JsonProperty("commercialName")]
        public string CommercialName { get; set; }

        [JsonProperty("electronicMiscDocType")]
        public Status ElectronicMiscDocType { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("reasonForIssuance")]
        public Status ReasonForIssuance { get; set; }

        [JsonProperty("subCode")]
        public string SubCode { get; set; }

        [JsonProperty("vendor")]
        public string Vendor { get; set; }
    }
}
