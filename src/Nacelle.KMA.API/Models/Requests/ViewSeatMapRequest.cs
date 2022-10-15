using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Requests
{
    public class ViewSeatMapRequest
    {
        [JsonProperty("returnSession")]
        public bool ReturnSession { get; set; }

        [JsonProperty("segmentIds")]
        public List<string> SegmentIds { get; set; }
    }
}
