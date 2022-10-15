#region Using Directives

using System;
using Nacelle.KMA.Core.ExtensionMethods;
using Newtonsoft.Json;

#endregion //Using Directives

namespace Nacelle.KMA.Core.Models.Entites
{
    public class SegmentEntity
    {
        #region Properties

        public string FromAirport { get; set; }
        public DateTime FromTime { get; set; }
        public string ToAirport { get; set; }
        public DateTime ToTime { get; set; }
        public DateTime BoardingTime { get; set; }
        public string Carrier { get; set; }
        public string FlightNumber { get; set; }
        public string Seat { get; set; }
        public string Gate { get; set; }
        public string Terminal { get; set; }
        public string Id { get; set; }
        public bool IsEligibleForCheckIn { get; set; }
        public bool HasQJump { get; set; }
        public bool HasCheckedIn { get; set; }
        public bool TicketIssued { get; set; }
        public string RecordLocator { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PassengerFlightId { get; set; }
        public bool HasFlightInTheFuture { get; set; }

        [JsonIgnore]
        public bool CanCheckIn => IsEligibleForCheckIn && FromTime.CanCheckIn();

        #endregion //Properties
    }
}