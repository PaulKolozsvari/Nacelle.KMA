using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class RegisterPushNotificationTokenResponse
    {
        [JsonProperty("Success")]
        public string Success { get; set; }

        [JsonProperty("Error")]
        public string Error { get; set; }
    }
}
