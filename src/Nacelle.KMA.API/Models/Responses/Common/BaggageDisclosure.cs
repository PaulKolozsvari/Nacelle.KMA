using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class BaggageDisclosure
    {
        [JsonProperty("checkedInBaggage")]
        public CheckedInBaggage CheckedInBaggage { get; set; }
    }
}
