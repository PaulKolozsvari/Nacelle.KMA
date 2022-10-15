using System;

namespace Nacelle.KMA.Core.Models.Items
{
    public class BoardingPassItem
    {
        public BoardingPassItem()
        {
            QrCode = "undefinded";
//#if DEBUG
//            QrCode = "M1SLODOWY ADAM M MR EGDYMRT JNBGRJMN 0903103J002A0001 100";
//#endif
        }


        public string PassengerName { get; set; }
        public string Seat { get; set; }
        public string Terminal { get; set; }
        public string Gate { get; set; }
        public string Zone { get; set; }
        public string FlightNumber { get; set; }
        public DateTime DepartureDateTime { get; set; }
        //public DateTime DepartureTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public string QrCode { get; set; }
        public DateTime BoardingTime { get; set; }

        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public string DepartureAirportCode { get; set; }
        public string ArrivalAirportCode { get; set; }

        public string BoardingPassJson { get; set; }
    }
}
