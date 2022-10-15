using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Common
{
    public class PersonName
    {
        [JsonProperty("first")]
        public string First { get; set; }

        [JsonProperty("last")]
        public string Last { get; set; }

        //[JsonProperty("raw")]
        //public string Raw { get; set; }

        public override string ToString() => $"{First} {Last}";
    }
}
