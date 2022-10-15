using System.Collections.Generic;
using System.Threading.Tasks;
using Nacelle.Core.Models;
using Nacelle.Core.Services;
using Nacelle.KMA.API.Configs;
using Nacelle.KMA.API.Models.Requests;
using Nacelle.KMA.API.Models.Responses;
using Nacelle.KMA.API.Exceptions.Factories;
using System;

namespace Nacelle.KMA.API.Services
{
    public class OpsApiService : Service, IOpsApiService
    {
        private string SessionToken { get; }

        public OpsApiService(IHttpClientService clientService, OpsApiConfig config) : base(clientService, config)
        {
            SessionToken = config.SessionKey;
            Headers = new Dictionary<string, string>()
            {
                {
                    Constants.Headers.SessionToken, SessionToken
                }
            };
        }

        public async Task<Response<WeatherResponse>> GetFiveDayWeatherForcastAsync(WeatherRequest request)
        {
            var url = string.Concat(Config.BaseUrl, "/", Constants.OpsApiConstants.PATH_Weather);


            var queryStringParams = new Dictionary<string, string>(QueryParams)
            {
                ["airportIATACode"] = request.AirportCode
            };

            var response = await HttpClientService.GetAsync<WeatherResponse>(queryStringParams, url, "GetFiveDayForeCast", Headers);

            if (!string.IsNullOrEmpty(response.Data.Error))
            {
                throw ExceptionFactory.General.TechnicalDifficulties();
            }

            return response;
        }

        public Task<Response<AirportResponse>> GetAirportsAsync()
        {
            return HttpClientService.GetAsync<AirportResponse>(QueryParams, Config.BaseUrl, "airports", Headers); 
        }
        
        public Task<Response<FlightStatusResponse>> GetFlightStatus(FlightStatusRequest request)
        {
            var url = string.Concat(Config.BaseUrl, "/", Constants.OpsApiConstants.PATH_Flight);
            
            var queryStringParams = new Dictionary<string, string>(QueryParams)
            {
                ["departureDate"] = request.DepartureDate.ToString("yyyy-MM-dd"),
                ["departureCode"] = request.DepartureCode,
                ["flightNumber"] = request.FlightNumber
            };
            
            return HttpClientService.GetAsync<FlightStatusResponse>(queryStringParams, url, "GetFlightStatus", Headers);
        }
    }
}
