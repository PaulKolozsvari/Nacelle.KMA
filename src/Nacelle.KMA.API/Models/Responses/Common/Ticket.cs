using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class Ticket
    {
        [JsonProperty("issued")]
        public bool Issued { get; set; }

        [JsonProperty("ticketCoupon")]
        public List<TicketCoupon> TicketCoupon { get; set; }

        [JsonProperty("ticketNumber")]
        public TicketNumber TicketNumber { get; set; }
    }
}
