using Xamarin.Essentials;

namespace Nacelle.KMA.Core.Caching
{
    public class AppSettings : IAppSettings
    {
        private const string PushNotificationsTokenKey = "pn_token_settings_key";

        public string PushNotificationsToken
        {
            get => Preferences.Get(PushNotificationsTokenKey, string.Empty);
            set => Preferences.Set(PushNotificationsTokenKey, value);
        }
    }
}
