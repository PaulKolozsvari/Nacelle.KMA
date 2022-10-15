using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nacelle.Core.Models;
using Nacelle.KMA.API.Models.Requests;
using Nacelle.KMA.API.Models.Responses;
using Nacelle.KMA.API.Services;
using Nacelle.KMA.Core.Caching;
using Nacelle.KMA.Core.Managers.Contracts;
using Nacelle.KMA.Core.Mappers;
using Nacelle.KMA.Core.Models.Entites;
using Nacelle.KMA.Core.Platform;

namespace Nacelle.KMA.Core.Managers
{
    public class WeatherManager : IWeatherManager
    {
        private readonly IOpsApiService _apiService;
        private readonly ICacheService _cacheService;
        private const string _weatherKey = "FiveDayWeatherForecast";

        public WeatherManager(IOpsApiService apiService, ICacheService cacheService)
        {
            _apiService = apiService;
            _cacheService = cacheService;
        }

        public async Task<List<WeatherEntity>> GetOrFetchWeatherData(string airportCode, bool forceRefresh = false)
        {
            if (string.IsNullOrWhiteSpace(airportCode))
                return null; //TODO: Negative Flow

            var airportCodeWeatherKey = $"{ _weatherKey }_{ airportCode.ToUpper()}";

            var weatherData = await _cacheService.GetOrUpdateValue<WeatherResponse>(airportCodeWeatherKey, () => FetchWeatherDataAsync(airportCode), 1, forceRefresh);

            if (weatherData == null || weatherData.Days == null || weatherData.Days.Count == 0)
            {
                var response = await FetchWeatherAsync(airportCode);

                if (response.IsSuccess)
                {
                    _cacheService.SetValue<WeatherResponse>(airportCodeWeatherKey, response.Data, 1);
                    return response.Data.ToWeatherEntity();
                }
            }

            return weatherData.ToWeatherEntity();
        }

        private async Task<WeatherResponse> FetchWeatherDataAsync(string airportCode)
        {
            var response = await FetchWeatherAsync(airportCode);
            return response.Data;
        }

        private Task<Response<WeatherResponse>> FetchWeatherAsync(string airportCode)
        {
            return _apiService.GetFiveDayWeatherForcastAsync(new WeatherRequest { AirportCode = airportCode });
        }
    }
}

