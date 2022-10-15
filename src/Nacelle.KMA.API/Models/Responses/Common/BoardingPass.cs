using Nacelle.KMA.API.Models.Common;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class BoardingPass
    {
        [JsonProperty("agent")]
        public Agent Agent { get; set; }

        [JsonProperty("barCode")]
        public string BarCode { get; set; }

        [JsonProperty("checkInSequenceNumber")]
        public string CheckInSequenceNumber { get; set; }

        [JsonProperty("deck")]
        public Deck Deck { get; set; }

        [JsonProperty("displayData")]
        public DisplayData DisplayData { get; set; }

        [JsonProperty("fareInfo")]
        public FareInfo FareInfo { get; set; }

        [JsonProperty("flightDetail")]
        public FlightDetail FlightDetail { get; set; }

        [JsonProperty("formattedBoardingPass", NullValueHandling = NullValueHandling.Ignore)]
        public FormattedBoardingPass FormattedBoardingPass { get; set; }

        [JsonProperty("personName")]
        public PersonName PersonName { get; set; }

        [JsonProperty("priorityVerificationCard")]
        public bool PriorityVerificationCard { get; set; }

        [JsonProperty("recordLocator")]
        public string RecordLocator { get; set; }

        [JsonProperty("seat")]
        public BoardingPassSeat Seat { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("supplementaryData")]
        public SupplementaryData SupplementaryData { get; set; }

        [JsonProperty("ticketCouponNumber")]
        public long TicketCouponNumber { get; set; }

        [JsonProperty("ticketNumber")]
        public TicketNumber TicketNumber { get; set; }
    }
}
