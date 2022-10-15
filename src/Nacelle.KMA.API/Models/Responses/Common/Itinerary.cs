using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class Itinerary
    {
        [JsonProperty("itineraryPart")]
        public List<ItineraryPart> ItineraryPart { get; set; }
    }
}
