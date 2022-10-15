using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class Eligibilities
    {
        [JsonProperty("eligibility")]
        public List<Eligibility> Eligibility { get; set; }
    }
}
