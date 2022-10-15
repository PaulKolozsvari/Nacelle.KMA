using System.Collections.Generic;
using System.Linq;
using Firebase.Analytics;
using Nacelle.KMA.Core.Analytics;

namespace Nacelle.KMA.iOS.Services
{
    public class FirebaseAnalyticsService : IAnalyticsService
    {
        public void LogEvent(string eventName, string target, Dictionary<string, string> context = null)
        {
            var parameters = new Dictionary<object, object>();

            if (!string.IsNullOrEmpty(target))
            {
                parameters.Add(nameof(target), target);
            }

            if (context != null)
            {
                foreach (var item in context)
                {
                    parameters.Add(item.Key, item.Value);
                }
            }

            Analytics.LogEvent(eventName, parameters.Count() > 0 ? parameters : null);
        }
    }
}
