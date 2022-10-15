#region Using Directives

using System;

#endregion //Using Directives

namespace Nacelle.KMA.Core.Models.Entites
{
    public class WeatherEntity
    {
        #region Properties

        public string Error { get; set; }
        public string AirportCode { get; set; }
        public string WeatherStation { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public DateTime DateTime { get; set; }
        public int CloudPercentage { get; set; }
        public double WindSpeed { get; set; }
        public double WindDirection { get; set; }
        public double Temperature { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public double RainVolume { get; set; }
        public double Pressure { get; set; }
        public double SeaLevelPressure { get; set; }
        public double GroundLevelPressure { get; set; }
        public double HumidityPercentage { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        #endregion //Properties
    }
}
