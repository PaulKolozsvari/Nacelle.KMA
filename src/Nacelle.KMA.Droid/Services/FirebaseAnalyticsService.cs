using System.Collections.Generic;
using Android.OS;
using Nacelle.KMA.Core.Analytics;

namespace Nacelle.KMA.Droid.Services
{
    public class FirebaseAnalyticsService : IAnalyticsService
    {
        public void LogEvent(string eventName, string target = "", Dictionary<string, string> context = null)
        {
            var bundleParameters = new Bundle();


            if (!string.IsNullOrEmpty(target))
            {
                bundleParameters.PutString(nameof(target), target);
            }

            if (context != null)
            {
                foreach (var item in context)
                {
                    bundleParameters.PutString(item.Key, item.Value);
                }
            }

            Firebase.Analytics.FirebaseAnalytics.GetInstance(Android.App.Application.Context).LogEvent(eventName, bundleParameters);
        }
    }
}
