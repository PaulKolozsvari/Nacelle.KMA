using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class TicketNumber
    {
        [JsonProperty("airlineAccountingCode", NullValueHandling = NullValueHandling.Ignore)]
        public long AirlineAccountingCode { get; set; }

        [JsonProperty("checkDigit", NullValueHandling = NullValueHandling.Ignore)]
        public long CheckDigit { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("serialNumber", NullValueHandling = NullValueHandling.Ignore)]
        public long SerialNumber { get; set; }
    }
}
