using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class ViewSeatMapResponse : BaseResponse<ViewSeatMapResult>
    {     
        [JsonProperty("seatMap")]
        public List<SeatMap> SeatMap { get; set; }
    }

    public class ViewSeatMapResult : Result
    {
        [JsonProperty("seatMapRef")]
        public string SeatMapRef { get; set; }
    }    

    public class SeatMap
    {
        [JsonProperty("cabin")]
        public List<Cabin> Cabins { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("requestedSegment")]
        public RequestedSegment RequestedSegment { get; set; }

        [JsonProperty("segment")]
        public Segment Segment { get; set; }
    }

    public class Cabin
    {
        [JsonProperty("bookingClass")]
        public string BookingClass { get; set; }

        [JsonProperty("column")]
        public List<Column> Columns { get; set; }

        [JsonProperty("row")]
        public List<Row> Rows { get; set; }
    }

    public class Column
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("characteristic", NullValueHandling = NullValueHandling.Ignore)]
        public List<Characteristic> Characteristics { get; set; }
    }

    public class Characteristic
    {
        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Row
    {
        [JsonProperty("characteristic", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Characteristics { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("slot")]
        public List<Slot> Slots { get; set; }
    }

    public class Slot
    {
        [JsonProperty("columnRef")]
        public string ColumnRef { get; set; }

        [JsonProperty("facility", NullValueHandling = NullValueHandling.Ignore)]
        public List<Facility> Facility { get; set; }

        [JsonProperty("position", NullValueHandling = NullValueHandling.Ignore)]
        public string Position { get; set; }

        [JsonProperty("seat", NullValueHandling = NullValueHandling.Ignore)]
        public Seat Seat { get; set; }
    }

    public class Facility
    {
        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Seat
    {
        [JsonProperty("limitation", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Limitations { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class RequestedSegment
    {
        [JsonProperty("airline")]
        public string Airline { get; set; }

        [JsonProperty("arrivalAirport")]
        public string ArrivalAirport { get; set; }

        [JsonProperty("bookingClass")]
        public string BookingClass { get; set; }

        [JsonProperty("departureAirport")]
        public string DepartureAirport { get; set; }

        [JsonProperty("departureDate")]
        public DateTimeOffset DepartureDate { get; set; }

        [JsonProperty("flightNumber")]
        public string FlightNumber { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("operatingAirline")]
        public string OperatingAirline { get; set; }

        [JsonProperty("operatingFlightNumber")]
        public string OperatingFlightNumber { get; set; }
    }

    public class ViewSeatsSegment
    {
        [JsonProperty("airline")]
        public string Airline { get; set; }

        [JsonProperty("arrivalAirport")]
        public string ArrivalAirport { get; set; }

        [JsonProperty("bookingClass")]
        public string BookingClass { get; set; }

        [JsonProperty("departureAirport")]
        public string DepartureAirport { get; set; }

        [JsonProperty("departureDate")]
        public DateTimeOffset DepartureDate { get; set; }

        [JsonProperty("equipment")]
        public string Equipment { get; set; }

        [JsonProperty("flightNumber")]
        public string FlightNumber { get; set; }
    }
}
