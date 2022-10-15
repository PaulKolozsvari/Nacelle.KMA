using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class PassengerSegments
    {
        [JsonProperty("passengerSegment")]
        public List<PassengerSegment> PassengerSegment { get; set; }
    }
}
