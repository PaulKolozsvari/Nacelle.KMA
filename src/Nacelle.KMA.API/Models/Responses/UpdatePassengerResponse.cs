using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class UpdatePassengerResponse : BaseResponse<UpdatePassengerResult>
    {        
    }

    public class UpdatePassengerResult : Result
    {
        [JsonProperty("update", NullValueHandling = NullValueHandling.Ignore)]
        public Update Update { get; set; }
    }
}
