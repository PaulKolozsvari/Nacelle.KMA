using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace Nacelle.KMA.API.Models.Responses
{
    public class FlightStatusResponse : BaseResponse<Result>
    {
        [JsonProperty("LatestDepartureDateTime")]
        public DateTimeOffset LatestDepartureDateTime { get; set; }

        [JsonProperty("LatestArrivalDateTime")]
        public DateTimeOffset LatestArrivalDateTime { get; set; }

        [JsonProperty("Status")]
        public string FlightStatus { get; set; }

        [JsonProperty("EstimatedTimeDeparture", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset EstimatedTimeDeparture { get; set; }

        [JsonProperty("EstimatedTimeArrival", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset EstimatedTimeArrival { get; set; }

        [JsonProperty("ActualTimeDeparture", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset ActualTimeDeparture { get; set; }

        [JsonProperty("ActualTimeArrival", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset ActualTimeArrival { get; set; }

        [JsonProperty("RunwayOff")]
        public string RunwayOff { get; set; }

        [JsonProperty("RunwayOn")]
        public string RunwayOn { get; set; }

        [JsonProperty("DepartureTerminal")]
        public string DepartureTerminal { get; set; }

        [JsonProperty("ArrivalTerminal")]
        public string ArrivalTerminal { get; set; }

        [JsonProperty("DepartureGate")]
        public string DepartureGate { get; set; }

        [JsonProperty("ArrivalGate")]
        public string ArrivalGate { get; set; }

        [JsonProperty("ScheduledTimeDeparture")]
        public DateTimeOffset ScheduledTimeDeparture { get; set; }

        [JsonProperty("ScheduledTimeArrival", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset ScheduledTimeArrival { get; set; }

        [JsonProperty("AirlineCode")]
        public string AirlineCode { get; set; }

        [JsonProperty("FlightNumber")]
        public string FlightNumber { get; set; }

        [JsonProperty("DepartureDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset DepartureDate { get; set; }

        [JsonProperty("DepartureCode")]
        public string DepartureCode { get; set; }

        [JsonProperty("ArrivalCode")]
        public string ArrivalCode { get; set; }
    }
}
