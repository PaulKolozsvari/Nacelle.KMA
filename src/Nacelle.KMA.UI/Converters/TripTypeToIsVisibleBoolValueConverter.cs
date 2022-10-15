using System;
using System.Globalization;
using MvvmCross.Forms.Converters;
using Nacelle.KMA.Core.Enums;
using Newtonsoft.Json;

namespace Nacelle.KMA.UI.Converters
{
    public class TripTypeToIsVisibleBoolValueConverter : MvxFormsValueConverter<TripType, bool>
    {
        protected override bool Convert(TripType value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(parameter is string) || string.IsNullOrEmpty(parameter.ToString()))
            {
                return false;
            }
            var parameterString = parameter.ToString().ToLower();
            var result = false;
            switch (value)
            {
                case TripType.Future:
                    result = ContaintsCheckIn(parameterString) || ContainsDateTime(parameterString);
                    break;
                case TripType.CheckInDayApproaching:
                    break;
                case TripType.CheckInDay:
                    result = ContaintsCheckIn(parameterString) || ContainsDateTime(parameterString) || ContainsWeather(parameterString);
                    break;
                case TripType.Boarding:
                    result = ContainsBoarding(parameterString) || ContainsGateCard(parameterString) || ContainsWeather(parameterString) | ContainsPassengerSeat(parameterString);
                    break;
                case TripType.LeavingSoon:
                    break;
                case TripType.Departing:
                    result = ContainsDeparting(parameterString) || ContainsGateCard(parameterString) || ContainsPassengerSeat(parameterString);
                    break;
                case TripType.Past:
                    break;
                case TripType.Delayed:
                    result = ContainsDelayed(parameterString) || ContainsGateCard(parameterString) || ContainsPassengerSeat(parameterString);
                    break;
                case TripType.Cancelled:
                    result = ContainsCancelled(parameterString) || ContainsGateCard(parameterString) || ContainsPassengerSeat(parameterString);
                    break;
                default:
                    break;
            }
            return result;
        }

        private bool ContaintsCheckIn(string parameter) => parameter.Contains("checkin");

        private bool ContainsWeather(string parameter) => parameter.Contains("weather");

        private bool ContainsDateTime(string parameter) => parameter.Contains("datetime");

        private bool ContainsBoarding(string parameter) => parameter.Contains("boarding");

        private bool ContainsDeparting(string parameter) => parameter.Contains("departing");

        private bool ContainsGateCard(string parameter) => parameter.Contains("gatecard");

        private bool ContainsPassengerSeat(string parameter) => parameter.Contains("passengerseat");

        private bool ContainsDelayed(string parameter) => parameter.Contains("delayed");

        private bool ContainsCancelled(string parameter) => parameter.Contains("cancelled");
    }
}
