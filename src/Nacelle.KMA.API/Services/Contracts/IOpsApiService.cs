using System.Threading.Tasks;
using Nacelle.Core.Models;
using Nacelle.KMA.API.Models.Requests;
using Nacelle.KMA.API.Models.Responses;

namespace Nacelle.KMA.API.Services
{
    public interface IOpsApiService
    {
        Task<Response<WeatherResponse>> GetFiveDayWeatherForcastAsync(WeatherRequest request);
        Task<Response<AirportResponse>> GetAirportsAsync();
        Task<Response<FlightStatusResponse>> GetFlightStatus(FlightStatusRequest request);
    }
}
