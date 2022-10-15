using System;
using Android.App;
using Android.Content.PM;
using Android.Gms.Common;
using Android.OS;
using Android.Runtime;
using Android.Views;
using FFImageLoading.Forms.Platform;
using MvvmCross.Forms.Platforms.Android.Views;
using Nacelle.KMA.Core.ViewModels;
using PanCardView.Droid;
using Refractored.XamForms.PullToRefresh.Droid;
using Xamarin.Forms;

namespace Nacelle.KMA.Droid
{
    [Activity(
        Theme = "@style/AppTheme",
        WindowSoftInputMode = SoftInput.StateAlwaysVisible | SoftInput.AdjustResize,
        ScreenOrientation = ScreenOrientation.Portrait,
        LaunchMode = LaunchMode.SingleInstance)]
    public class AppActivity : MvxFormsAppCompatActivity<AppViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            FormsMaterial.Init(this, bundle);
            PullToRefreshLayoutRenderer.Init();
            Rg.Plugins.Popup.Popup.Init(this, bundle);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            CachedImageRenderer.Init(true);
            CardsViewRenderer.Preserve();
            Xamarin.Essentials.Platform.Init(this, bundle); // add this line to your code, it may also be called: bundle
            XF.Material.Droid.Material.Init(this, bundle);

            IsPlayServicesAvailable();

            CreateNotificationChannel();

            //ImageService.Instance.LoadEmbeddedResource("resource://Nacelle.KMA.UI.Resources.Images.kma-logo.svg");

            //Window.SetSoftInputMode(SoftInput.AdjustResize);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public bool IsPlayServicesAvailable()
        {
            var isAvailable = false;
            var resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            string messageText;
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    messageText = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    messageText = "This device is not supported";
                    Finish();
                }
            }
            else
            {
                messageText = "Google Play Services is available.";
                isAvailable = true;
            }

            Console.WriteLine($"Play Services Avaiable message text: {messageText}");
            return isAvailable;
        }

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channel = new NotificationChannel("channel_id",
                                                  "FCM Notifications",
                                                  NotificationImportance.Default)
            {

                Description = "Firebase Cloud Messages appear in this channel"
            };

            var notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        //{
        //    Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}

        public override void OnBackPressed()
        {
            Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
        }
    }
}
