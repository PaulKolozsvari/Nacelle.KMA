using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class CheckInResponse : BaseResponse<CheckInResult>
    {
        [JsonProperty("boardingPasses")]
        public List<BoardingPassElement> BoardingPasses { get; set; }

        [JsonProperty("reservation")]
        public Reservation Reservation { get; set; }
    }

    public class CheckInResult : Result
    {
        [JsonProperty("passengerFlightRef")]
        public string PassengerFlightRef { get; set; }
    }

    public class BoardingPassFareInfo
    {
        [JsonProperty("bookingClass")]
        public string BookingClass { get; set; }
    }

    public class BookedSeat
    {
        [JsonProperty("column")]
        public string Column { get; set; }

        [JsonProperty("row")]
        public int Row { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class BoardingPassTicketNumber
    {
        [JsonProperty("airlineAccountingCode")]
        public string AirlineAccountingCode { get; set; }

        [JsonProperty("checkDigit")]
        public string CheckDigit { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("serialNumber")]
        public string SerialNumber { get; set; }
    }
}
