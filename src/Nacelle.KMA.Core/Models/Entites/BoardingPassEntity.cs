using System;

namespace Nacelle.KMA.Core.Models.Entites
{
    public class BoardingPassEntity
    {
        #region Properties

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
        public string PassengerFlightId { get; set; }
        public string PassengerId { get; set; }

        #endregion //Properties
    }
}
