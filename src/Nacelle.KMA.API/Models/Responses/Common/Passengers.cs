using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class Passengers
    {
        [JsonProperty("passenger")]
        public List<Passenger> Passenger { get; set; }
    }
}
