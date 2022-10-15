using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
namespace Nacelle.KMA.API.Models.Responses
{
    public class WeatherResponse 
    {
        [JsonProperty("Error")]
        public string Error { get; set; }

        [JsonProperty("AirportCode")]
        public string AirportCode { get; set; }

        [JsonProperty("WeatherStation")]
        public string WeatherStation { get; set; }

        [JsonProperty("Longitude")]
        public double Longitude { get; set; }

        [JsonProperty("Latitude")]
        public double Latitude { get; set; }

        [JsonProperty("Days")]
        public List<Days> Days { get; set; }
    }

    public class Days
    {
        [JsonProperty("DateTime")]
        public DateTime DateTime { get; set; }

        [JsonProperty("CloudPercentage")]
        public int CloudPercentage { get; set; }

        [JsonProperty("WindSpeed")]
        public double WindSpeed { get; set; }

        [JsonProperty("WindDirection")]
        public double WindDirection { get; set; }

        [JsonProperty("Temperature")]
        public double Temperature { get; set; }

        [JsonProperty("MinTemperature")]
        public double MinTemperature { get; set; }

        [JsonProperty("MaxTemperature")]
        public double MaxTemperature { get; set; }

        [JsonProperty("RainVolume")]
        public double RainVolume { get; set; }

        [JsonProperty("Pressure")]
        public double Pressure { get; set; }

        [JsonProperty("SeaLevelPressure")]
        public double SeaLevelPressure { get; set; }

        [JsonProperty("GroundLevelPressure")]
        public double GroundLevelPressure { get; set; }

        [JsonProperty("HumidityPercentage")]
        public double HumidityPercentage { get; set; }

        [JsonProperty("Main")]
        public string Main { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Icon")]
        public string Icon { get; set; }
    }
}
