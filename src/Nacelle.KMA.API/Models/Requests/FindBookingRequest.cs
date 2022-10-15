using System;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Requests
{
    public partial class FindBookingRequest
    {
        [JsonProperty("reservationCriteria")]
        public ReservationCriteria ReservationCriteria { get; set; }
    }

    public partial class ReservationCriteria
    {
        private string _recordLocator;

        [JsonProperty("recordLocator")]
        public string RecordLocator
        {
            get => string.IsNullOrWhiteSpace(_recordLocator) ? _recordLocator : _recordLocator.ToUpper();
            set => _recordLocator = value;
        }

        [JsonProperty("lastName")]
        public string LastName { get; set; }
    }
}
