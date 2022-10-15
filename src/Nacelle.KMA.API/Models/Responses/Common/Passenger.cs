using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Responses
{
    public class Passenger
    {
        [JsonProperty("contactDetails")]
        public ContactDetails ContactDetails { get; set; }

        [JsonProperty("eligibilities")]
        public Eligibilities Eligibilities { get; set; }

        [JsonProperty("fiscalCode")]
        public string FiscalCode { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("nameNumber")]
        public string NameNumber { get; set; }

        [JsonProperty("passengerSegments")]
        public PassengerSegments PassengerSegments { get; set; }

        [JsonProperty("personName")]
        public PassengerPersonName PersonName { get; set; }

        [JsonProperty("syntheticIdentifier")]
        public string SyntheticIdentifier { get; set; }

        [JsonProperty("ticket")]
        public List<Ticket> Ticket { get; set; }

        [JsonProperty("transportationSecurity")]
        public TransportationSecurity TransportationSecurity { get; set; }

        [JsonProperty("type")]
        public TypeClass Type { get; set; }

        [JsonProperty("weightCategory", NullValueHandling = NullValueHandling.Ignore)]
        public string WeightCategory { get; set; }
    }
}
