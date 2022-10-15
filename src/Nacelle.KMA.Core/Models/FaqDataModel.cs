using Newtonsoft.Json;

namespace Nacelle.KMA.Core.Models
{
    public partial class FaqDataModel
    {
        [JsonProperty("questions")]
        public QuestionItem[] QuestionItems { get; set; }
    }
}
