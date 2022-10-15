using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Firebase.Messaging;

namespace Nacelle.KMA.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class KmaFirebaseMessagingService : FirebaseMessagingService
    {
        internal static readonly string CHANNEL_ID = "PNChannel";

        public override void OnMessageReceived(RemoteMessage message)
        {
            var notification = message.GetNotification();

            //Log.Debug(TAG, "From: " + message.From);
            if (notification != null)
            {
                //These is how most messages will be received
                //Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
                SendNotification(notification.Title ?? string.Empty, notification.Body);
            }
            else
            {
                //Only used for debugging payloads sent from the Azure portal
                SendNotification(string.Empty, message.Data.Values.First());
            }
        }

        private void SendNotification(string messageTitle, string messageBody)
        {
            /*var pnService = Mvx.IoCProvider.Resolve<INotificationService>();

			pnService.RemoteNotificationReceived(messageTitle, messageBody);*/

            var intent = new Intent(this, typeof(AppActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            var title = $"{ApplicationInfo.LoadLabel(PackageManager)} Notification";

            var notificationBuilder = new NotificationCompat.Builder(this, CHANNEL_ID)
                        .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate)
                        .SetContentTitle(title)
                        .SetSmallIcon(Resource.Drawable.TabIconHome)
                        .SetContentText(messageBody)
                        .SetAutoCancel(true)
                        .SetContentIntent(pendingIntent)
                        .SetPriority((int)NotificationImportance.High);

            var notificationManager = NotificationManager.FromContext(this);

            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }
}
