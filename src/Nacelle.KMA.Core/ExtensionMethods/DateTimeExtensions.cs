#region Using Directives

using System;
using System.Runtime.InteropServices;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ExtensionMethods
{
    public static class DateTimeExtensions
    {
        #region Methods

        public static bool CanCheckIn(this DateTime dateTime)
        {
            var timeDiff = dateTime.Subtract(DateTime.Now);

            var canCheckIn = timeDiff.IsIn24HourWindow();

            return canCheckIn;
        }

        public static bool IsIn24HourWindow(this TimeSpan timeSpan)
        {
            var totalHours = timeSpan.TotalHours;
            return totalHours >= 1 && totalHours <= 24;
        }

        public static bool IsIn48HourWindow(this TimeSpan timeSpan)
        {
            var totalHours = timeSpan.TotalHours;
            return totalHours > 24 && totalHours <= 48;
        }

        public static bool IsFutureBooking(this TimeSpan timeSpan)
        {
            var totalDays = timeSpan.TotalDays;
            return totalDays >= 1;
        }

        public static bool IsFutureBooking(this DateTime dateTime)
        {
            var timespan = dateTime - DateTime.Now.RoundDownMinute();
            return timespan.IsFutureBooking();
        }

        public static bool IsDayOfOperations(this DateTime departureDate)
        {
            var result = DateTime.Now.Date.Equals(departureDate.Date);
            return result;
        }

        public static DateTime RoundUpMinute(this DateTime dateTime)
        {
            if (dateTime.Second == 0)
            {
                return dateTime;
            }
            var result = dateTime.AddSeconds(60 - dateTime.Second);
            return result;
        }

        public static DateTime RoundDownMinute(this DateTime dateTime)
        {
            var result = dateTime.AddSeconds(-dateTime.Second);
            return result;
        }

        #endregion //Methods
    }
}
