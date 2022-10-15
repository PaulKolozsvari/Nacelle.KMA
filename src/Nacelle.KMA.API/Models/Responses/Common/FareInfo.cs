using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class FareInfo
    {
        [JsonProperty("bookingClass")]
        public string BookingClass { get; set; }

        [JsonProperty("brand", NullValueHandling = NullValueHandling.Ignore)]
        public string Brand { get; set; }

        [JsonProperty("cabinClass", NullValueHandling = NullValueHandling.Ignore)]
        public string CabinClass { get; set; }
    }
}
