using System;
using System.Collections.Generic;

namespace Nacelle.KMA.Core.Builders
{
    public class AnalyticsContextBuilder
    {
        private string _reference;
        private string _flightNo;
        private string _origin;
        private string _destination;
        private string _departureDate;
        private string _departureTime;

        public Dictionary<string, string> Build()
        {
            var contextDictionary = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(_reference))
            {
                contextDictionary.Add(Constants.Analytics.Context.BookingReference, _reference);
            }

            if (!string.IsNullOrEmpty(_flightNo))
            {
                contextDictionary.Add(Constants.Analytics.Context.FlightNumber, _flightNo);
            }

            if (!string.IsNullOrEmpty(_origin))
            {
                contextDictionary.Add(Constants.Analytics.Context.Origin, _origin);
            }

            if (!string.IsNullOrEmpty(_destination))
            {
                contextDictionary.Add(Constants.Analytics.Context.Destination, _destination);
            }

            if (!string.IsNullOrEmpty(_departureDate))
            {
                contextDictionary.Add(Constants.Analytics.Context.DepartureDate, _departureDate);
            }

            if (!string.IsNullOrEmpty(_departureTime))
            {
                contextDictionary.Add(Constants.Analytics.Context.DepartureTime, _departureTime);
            }

            return contextDictionary;
        }

        public AnalyticsContextBuilder WithBookingReference(string bookingReference)
        {
            _reference = bookingReference;
            return this;
        }

        public AnalyticsContextBuilder WithFlightNo(string flightNo)
        {
            _flightNo = flightNo;
            return this;
        }

        public AnalyticsContextBuilder WithOrigin(string origin)
        {
            _origin = origin;
            return this;
        }

        public AnalyticsContextBuilder WithDestination(string destination)
        {
            _destination = destination;
            return this;
        }

        public AnalyticsContextBuilder WithDepartureDate(DateTime departureDate)
        {
            _departureDate = departureDate.ToString("yyyy-MM-dd");
            return this;
        }

        public AnalyticsContextBuilder WithDepartureTime(DateTime departureTime)
        {
            _departureTime = departureTime.ToString("HH:mm");
            return this;
        }
    }
}
