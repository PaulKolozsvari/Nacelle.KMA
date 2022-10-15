using System;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class PreScreeningData
    {
        [JsonProperty("dateOfBirth")]
        public DateTimeOffset DateOfBirth { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("personName")]
        public PreScreeningDataPersonName PersonName { get; set; }
    }
}
