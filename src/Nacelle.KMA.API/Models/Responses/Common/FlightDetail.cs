using System;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class FlightDetail
    {
        [JsonProperty("airline")]
        public string Airline { get; set; }

        [JsonProperty("arrivalAirport")]
        public string ArrivalAirport { get; set; }

        [JsonProperty("arrivalCountry")]
        public string ArrivalCountry { get; set; }

        [JsonProperty("arrivalTime")]
        public DateTimeOffset ArrivalTime { get; set; }

        [JsonProperty("boardingTime")]
        public DateTimeOffset BoardingTime { get; set; }

        [JsonProperty("commuter")]
        public bool Commuter { get; set; }

        [JsonProperty("departureAirport")]
        public string DepartureAirport { get; set; }

        [JsonProperty("departureCountry")]
        public string DepartureCountry { get; set; }

        [JsonProperty("departureFlightScheduleStatus")]
        public string DepartureFlightScheduleStatus { get; set; }

        [JsonProperty("departureGate")]
        public string DepartureGate { get; set; }

        [JsonProperty("departureTime")]
        public DateTimeOffset DepartureTime { get; set; }

        [JsonProperty("equipment")]
        public string Equipment { get; set; }

        [JsonProperty("estimatedArrivalTime")]
        public DateTimeOffset EstimatedArrivalTime { get; set; }

        [JsonProperty("estimatedDepartureTime")]
        public DateTimeOffset EstimatedDepartureTime { get; set; }

        [JsonProperty("flightNumber")]
        public string FlightNumber { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("operatingAirline")]
        public string OperatingAirline { get; set; }

        [JsonProperty("operatingAirlineName", NullValueHandling = NullValueHandling.Ignore)]
        public string OperatingAirlineName { get; set; }

        [JsonProperty("operatingFlightNumber")]
        public string OperatingFlightNumber { get; set; }

        [JsonProperty("departureTerminal", NullValueHandling = NullValueHandling.Ignore)]
        public string DepartureTerminal { get; set; }
    }
}
