using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class Reservation
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("itinerary")]
        public Itinerary Itinerary { get; set; }

        [JsonProperty("passengers")]
        public Passengers Passengers { get; set; }

        [JsonProperty("recordLocator")]
        public string RecordLocator { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }
}
