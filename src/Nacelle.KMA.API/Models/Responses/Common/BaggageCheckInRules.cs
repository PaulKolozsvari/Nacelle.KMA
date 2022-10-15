using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class BaggageCheckInRules
    {
        [JsonProperty("homePrintedBagTagRestricted")]
        public HomePrintedBagTagRestricted HomePrintedBagTagRestricted { get; set; }

        [JsonProperty("lateCheckIn")]
        public bool LateCheckIn { get; set; }

        [JsonProperty("petAllowed")]
        public bool PetAllowed { get; set; }
    }
}
