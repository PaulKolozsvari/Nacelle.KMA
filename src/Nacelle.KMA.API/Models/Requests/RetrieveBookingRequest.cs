using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Requests
{
    public class RetrieveBookingRequest
    {
        private string _recordLocator;

        [JsonProperty("rloc")]
        public string RecordLocator
        {
            get => string.IsNullOrWhiteSpace(_recordLocator) ? _recordLocator : _recordLocator.ToUpper();
            set => _recordLocator = value;
        }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("deviceID")]
        public string DeviceId { get; set; }
    }
}
