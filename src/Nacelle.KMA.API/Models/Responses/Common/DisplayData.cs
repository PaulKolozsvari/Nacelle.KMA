using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class DisplayData
    {
        [JsonProperty("agentCityName")]
        public string AgentCityName { get; set; }

        [JsonProperty("arrivalAirportName")]
        public string ArrivalAirportName { get; set; }

        [JsonProperty("boardingTime")]
        public string BoardingTime { get; set; }

        [JsonProperty("departureAirportName")]
        public string DepartureAirportName { get; set; }

        [JsonProperty("documentTypeText")]
        public string DocumentTypeText { get; set; }

        [JsonProperty("estimatedDepartureDate")]
        public string EstimatedDepartureDate { get; set; }

        [JsonProperty("estimatedDepartureTime")]
        public string EstimatedDepartureTime { get; set; }

        [JsonProperty("exitText")]
        public string ExitText { get; set; }

        [JsonProperty("operatingAirlineName")]
        public string OperatingAirlineName { get; set; }

        [JsonProperty("pingTipText")]
        public string PingTipText { get; set; }

        [JsonProperty("scheduledArrivalTime")]
        public string ScheduledArrivalTime { get; set; }

        [JsonProperty("ticketTypeText")]
        public string TicketTypeText { get; set; }
    }
}
