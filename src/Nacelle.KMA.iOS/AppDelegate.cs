using System;
using FFImageLoading.Forms.Platform;
using FFImageLoading.Svg.Forms;
using Firebase.CloudMessaging;
using Foundation;
using MvvmCross;
using MvvmCross.Forms.Platforms.Ios.Core;
using Nacelle.KMA.Core.Caching;
using Nacelle.KMA.Core.Managers;
using PanCardView.iOS;
using Refractored.XamForms.PullToRefresh.iOS;
using UIKit;
using UserNotifications;

namespace Nacelle.KMA.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxFormsApplicationDelegate<Setup, Core.App, UI.App>, IMessagingDelegate, IUNUserNotificationCenterDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            var result = base.FinishedLaunching(app, options);

            global::Xamarin.Forms.FormsMaterial.Init();
            PullToRefreshLayoutRenderer.Init();
            Rg.Plugins.Popup.Popup.Init();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            CachedImageRenderer.Init();
            CardsViewRenderer.Preserve();
            XF.Material.iOS.Material.Init();

            var ignore = typeof(SvgCachedImage); // For Linking Purposes

            SetTabBarStyle();

            global::Firebase.Core.App.Configure();
            UNUserNotificationCenter.Current.Delegate = this;
            var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
            UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) =>
            {
                Console.WriteLine(granted);
            });
            UIApplication.SharedApplication.RegisterForRemoteNotifications();
            Messaging.SharedInstance.Delegate = this;

            return result;
        }

        public override void DidRegisterUserNotificationSettings(UIApplication application, UIUserNotificationSettings notificationSettings)
        {
            var token = Messaging.SharedInstance.FcmToken ?? "";
            Console.WriteLine($"FCM token: {token}");
        }

        [Export("messaging:didReceiveRegistrationToken:")]
        public async void DidReceiveRegistrationToken(Messaging messaging, string fcmToken)
        {
            Console.WriteLine($"Firebase registration token: {fcmToken}");

            //Persist the FCM token to user prefs for easy retrieval.
            var appSettings = Mvx.IoCProvider.Resolve<IAppSettings>();

            if (!appSettings.PushNotificationsToken.Equals(fcmToken))
            {
                appSettings.PushNotificationsToken = fcmToken;

                var notificationsManager = Mvx.IoCProvider.Resolve<INotificationsManager>();
                await notificationsManager.RegisterPushNotificationsTokenAsync(fcmToken, "IOS");
            }

            // Note: This callback is fired at each app startup and whenever a new token is generated.
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            Messaging.SharedInstance.ApnsToken = deviceToken;
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            //TODO: Possbily display In-app toast
            Console.WriteLine($"APS alert: {userInfo.ValueForKeyPath(new NSString("aps.alert")).ToString()}");
        }

        private static void SetTabBarStyle()
        {
            //UITabBar.Appearance.SelectedImageTintColor = UIColor.FromRGB(140, 198, 63);
            //UITabBar.Appearance.BarTintColor = UIColor.FromRGB(249, 249, 249);
            UITabBar.Appearance.TintColor = UIColor.FromRGB(140, 198, 63);

            UITabBarItem.Appearance.SetTitleTextAttributes(
                new UITextAttributes
                {
                    TextColor = UIColor.FromRGB(140, 198, 63)
                },
                UIControlState.Selected);

            UITabBarItem.Appearance.SetTitleTextAttributes(
                new UITextAttributes
                {
                    TextColor = UIColor.FromRGB(102, 102, 102)

                },
                UIControlState.Normal);
        }
    }
}
