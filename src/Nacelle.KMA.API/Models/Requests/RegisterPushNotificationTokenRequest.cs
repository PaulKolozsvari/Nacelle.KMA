using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Requests
{
    public class RegisterPushNotificationTokenRequest
    {
        [JsonProperty("DeviceIdentity")]
        public string DeviceIdentity { get; set; }

        [JsonProperty("DeviceType")]
        public string DeviceType { get; set; }
    }
}
