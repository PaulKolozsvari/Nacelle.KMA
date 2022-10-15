using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class SupplementaryData
    {
        [JsonProperty("exclusiveWaitingArea")]
        public bool ExclusiveWaitingArea { get; set; }

        [JsonProperty("loungeAccess")]
        public bool LoungeAccess { get; set; }

        [JsonProperty("spanishLargeFamily")]
        public bool SpanishLargeFamily { get; set; }

        [JsonProperty("spanishResident")]
        public bool SpanishResident { get; set; }
    }
}
