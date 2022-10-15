using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class SelectSeatsResponse : BaseResponse<Result>
    {
        [JsonProperty("reservation")]
        public Reservation Reservation { get; set; }
    }
}
