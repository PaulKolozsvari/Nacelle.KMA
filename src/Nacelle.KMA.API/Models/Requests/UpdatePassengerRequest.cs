using System.Collections.Generic;
using System.Linq;
using Nacelle.KMA.API.Models.Common;
using Newtonsoft.Json;

namespace Nacelle.KMA.API.Models.Requests
{
    public class UpdatePassengerRequest
    {
        [JsonProperty("returnSession")]
        public bool ReturnSession { get; set; }

        [JsonProperty("passengerDetails")]
        public List<UpdatePassengerDetails> PassengerDetails { get; set; }
    }

    public class UpdatePassengerDetails
    {
        [JsonProperty("passengerId")]
        public string PassengerId { get; set; }

        [JsonProperty("documents", NullValueHandling = NullValueHandling.Ignore)]
        public List<Documents> Documents { get; set; }

        //[JsonProperty("addresses")]
        //public List<Address> Addresses { get; set; }

        [JsonProperty("emergencyContacts", NullValueHandling = NullValueHandling.Ignore)]
        public List<EmergencyContact> EmergencyContacts { get; set; }

        //[JsonProperty("loyaltyAccounts")]
        //public List<LoyaltyAccount> LoyaltyAccounts { get; set; }

        [JsonProperty("weightCategory")]
        public string WeightCategory { get; set; }

        //[JsonProperty("transportationSecurity")]
        //public TransportationSecurity TransportationSecurity { get; set; }

        public bool ShouldSerializeDocuments()
        {
            return (Documents != null && Documents.Any());
        }

        public bool ShouldSerializeEmergencyContacts()
        {
            return (EmergencyContacts != null && EmergencyContacts.Any());
        }
    }

    public class TransportationSecurity
    {
        [JsonProperty("preScreeningData")]
        public PreScreeningData PreScreeningData { get; set; }

        [JsonProperty("redressNumber")]
        public string RedressNumber { get; set; }

        [JsonProperty("knownTravelerNumber")]
        public string KnownTravelerNumber { get; set; }
    }

    [JsonObject("preScreeningData")]
    public class PreScreeningData
    {
        [JsonProperty("personName")]
        public PersonName PersonName { get; set; }

        [JsonProperty("dateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }
    }

    public class Documents
    {
        [JsonProperty("document")]
        public Document Document { get; set; }
    }

    public class Document
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("personName")]
        public PersonName PersonName { get; set; }

        [JsonProperty("nationality")]
        public string Nationality { get; set; }

        [JsonProperty("countryOfBirth")]
        public string CountryOfBirth { get; set; }

        [JsonProperty("placeOfBirth")]
        public string PlaceOfBirth { get; set; }

        [JsonProperty("dateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonProperty("issuingCountry")]
        public string IssuingCountry { get; set; }

        [JsonProperty("expiryDate")]
        public string ExpiryDate { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }
    }

    public class Address
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("street1")]
        public string Street1 { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("stateProvince")]
        public string StateProvince { get; set; }
    }

    public class EmergencyContact
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("contactDetails")]
        public string ContactDetails { get; set; }
    }

    public class LoyaltyAccount
    {
        [JsonProperty("memberAirline")]
        public string MemberAirline { get; set; }

        [JsonProperty("memberId")]
        public string MemberId { get; set; }
    }
}
