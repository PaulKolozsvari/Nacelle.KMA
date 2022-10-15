using Newtonsoft.Json;

namespace Nacelle.KMA.Core.Models
{
    public partial class QuestionItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }
    }
}
