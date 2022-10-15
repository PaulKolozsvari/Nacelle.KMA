using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class RetrieveBookingIXResponse : BaseResponse<Result>
    {
        [JsonProperty("bookings")]
        public Bookings Bookings { get; set; }
    }

    public class Bookings
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("booking")]
        public List<Booking> Booking { get; set; }
    }

    public class Booking
    {
        [JsonProperty("rloc")]
        public string Rloc { get; set; }

        [JsonProperty("bookingName")]
        public List<BookingName> BookingName { get; set; }
    }

    public class BookingName
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("passengerType")]
        public long PassengerType { get; set; }

        [JsonProperty("bookingNameItem")]
        public List<BookingNameItem> BookingNameItem { get; set; }
    }

    public class BookingNameItem
    {
        [JsonProperty("aircraftType")]
        public long AircraftType { get; set; }

        [JsonProperty("arrivalDateTime")]
        public DateTimeOffset ArrivalDateTime { get; set; }

        [JsonProperty("commercialCarrier")]
        public string CommercialCarrier { get; set; }

        [JsonProperty("commercialFlightNumber")]
        public long CommercialFlightNumber { get; set; }

        [JsonProperty("dateItemTicketed")]
        public DateTimeOffset DateItemTicketed { get; set; }

        [JsonProperty("departureDate")]
        public DateTimeOffset DepartureDate { get; set; }

        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("destinationCity")]
        public string DestinationCity { get; set; }

        [JsonProperty("destinationCountry")]
        public string DestinationCountry { get; set; }

        [JsonProperty("operatingCarrier")]
        public string OperatingCarrier { get; set; }

        [JsonProperty("operatingFlightNumber")]
        public string OperatingFlightNumber { get; set; }

        [JsonProperty("origin")]
        public string Origin { get; set; }

        [JsonProperty("originCity")]
        public string OriginCity { get; set; }

        [JsonProperty("originCountry")]
        public string OriginCountry { get; set; }

        [JsonIgnore]
        public bool HasFlightInTheFuture => DepartureDate > DateTimeOffset.Now; 
    }
}
