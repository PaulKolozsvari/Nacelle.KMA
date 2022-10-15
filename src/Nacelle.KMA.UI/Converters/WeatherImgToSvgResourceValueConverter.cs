using System;
using System.Globalization;
using MvvmCross.Forms.Converters;
using System.Diagnostics;

namespace Nacelle.KMA.UI.Converters
{
    public class WeatherImgToSvgResourceValueConverter : MvxFormsValueConverter<string, string>
    {
        protected override string Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            const string basePath = "resource://Nacelle.KMA.UI.Resources.Images.";
            var filename = value.Replace("http://openweathermap.org/img/w/", string.Empty);
            var result = $"{basePath}{MapImageToSvgFileName(filename ??value)}";
            Debug.WriteLine("WeatherImgToSvgResourceValueConverter: " + result);
            return result;
        }

        private string MapImageToSvgFileName(string fileName)
        {
            switch (fileName)
            {
                case "01d.png": return "weather-day-clear-sky.svg";
                case "02d.png": return "weather-day-few-clouds.svg";
                case "03d.png": return "weather-day-scattered-clouds.svg";
                case "04d.png": return "weather-day-broken-clouds.svg";
                case "09d.png": return "weather-day-shower-rain.svg";
                case "10d.png": return "weather-day-rain.svg";
                case "11d.png": return "weather-day-thunderstorm.svg";
                case "13d.png": return "weather-day-snow.svg";
                case "50d.png": return "weather-day-mist.svg";
                case "01n.png": return "weather-night-clear-sky.svg";
                case "02n.png": return "weather-night-few-clouds.svg";
                case "03n.png": return "weather-night-scattered-clouds.svg";
                case "04n.png": return "weather-night-broken-clouds.svg";
                case "09n.png": return "weather-night-shower-rain.svg";
                case "10n.png": return "weather-night-rain.svg";
                case "11n.png": return "weather-night-thunderstorm.svg";
                case "13n.png": return "weather-night-snow.svg";
                case "50n.png": return "weather-night-mist.svg";
                default: return "weather-day-clear-sky.svg";
            }

        }
    }
}
