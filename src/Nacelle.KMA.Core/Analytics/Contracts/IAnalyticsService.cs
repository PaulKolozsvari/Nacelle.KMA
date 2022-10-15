using System.Collections.Generic;

namespace Nacelle.KMA.Core.Analytics
{
    public interface IAnalyticsService
    {
        void LogEvent(string eventName, string target = "", Dictionary<string, string> context = null);
    }
}
