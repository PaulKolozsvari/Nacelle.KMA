using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Android.App;
using Firebase.Iid;
using MvvmCross;
using MvvmCross.ViewModels;
using Nacelle.KMA.Core.Caching;
using Nacelle.KMA.Core.Managers;
using ZXing.OneD;

namespace Nacelle.KMA.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class KmaFirebaseIIDService : FirebaseInstanceIdService
    {
        const string _tag = "KmaFirebaseIIDService";
        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Debug.WriteLine(_tag, "Refreshed token: " + refreshedToken);

            MvxNotifyTask.Create(() => SendRegistrationToServerAsync(refreshedToken));
        }

        private async Task SendRegistrationToServerAsync(string token)
        {
            // Potentially too early to resolve from IOC container, so wait a few seconds.
            await Task.Delay(TimeSpan.FromSeconds(2));
            var appSettings = Mvx.IoCProvider.Resolve<IAppSettings>();
            if (!appSettings.PushNotificationsToken.Equals(token))
            {
                appSettings.PushNotificationsToken = token;

                // Potentially too early to resolve from IOC container
                var notificationsManager = Mvx.IoCProvider.Resolve<INotificationsManager>();
                await notificationsManager.RegisterPushNotificationsTokenAsync(token, "ANDROID");
            }
        }
    }
}
