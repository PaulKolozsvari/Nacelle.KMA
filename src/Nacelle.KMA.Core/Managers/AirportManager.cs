using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nacelle.Core.Models;
using Nacelle.KMA.API.Models.Responses;
using Nacelle.KMA.API.Services;
using Nacelle.KMA.Core.Caching;
using Nacelle.KMA.Core.Managers.Contracts;
using Nacelle.KMA.Core.Mappers;
using Nacelle.KMA.Core.Models.Entites;

namespace Nacelle.KMA.Core.Managers
{
    public class AirportManager : IAirportManager
    {
        private readonly IOpsApiService _apiService;
        private readonly IConnectivityManager _connectivityManager;
        private readonly ICacheService _cacheService;

        public AirportManager(IOpsApiService apiService, ICacheService cacheService, IConnectivityManager connectivityManager)
        {
            _cacheService = cacheService;
            _connectivityManager = connectivityManager;
            _apiService = apiService;
        }

        public async Task<AirportEntity> GetAirport(string airportCode, bool forceRefresh = false)
        {
            if (_connectivityManager.NetworkAccess != Enums.NetworkAccess.Internet)
            {
                //TODO: Handle reachability here! 
            }

            if (string.IsNullOrWhiteSpace(airportCode))
                return null; //TODO: Negative Flow

            //TODO: Should we cache this data and for how long?
            var airportData = await _cacheService.GetOrUpdateValue<AirportResponse>("airports", () => GetAirportsDataAsync(), 5, forceRefresh);

            if (airportData == null)
            {
                return await FetchRemoteAirportEntity(airportCode);
            }

            //Locally stored Airports
            return FetchLocalAirportEntity(airportCode, airportData.ToAirportEntity());
        }

        private async Task<AirportResponse> GetAirportsDataAsync()
        {
            return (await GetAirportsAsync()).Data;
        }

        private async Task<Response<AirportResponse>> GetAirportsAsync()
        {
            return await _apiService.GetAirportsAsync();
        }

        private AirportEntity FetchLocalAirportEntity(string airportCode, List<AirportEntity> entities)
        {
            var airport = GetAirport(airportCode, entities);
            if (airport == null)
            {
                //TODO: Airport not found
                return null;
            }

            return airport;
        }

        private async Task<AirportEntity> FetchRemoteAirportEntity(string airportCode)
        {
            var response = await GetAirportsAsync();

            if (response.IsSuccess)
            {
                //Fetched Airports
                var airportEntity = GetAirport(airportCode, response.Data.ToAirportEntity());
                if (airportEntity == null)
                {
                    //TODO: Airport not found
                    return null;
                }
                return airportEntity;
            }
            return null;
        }

        private AirportEntity GetAirport(string airportCode, List<AirportEntity> entities) => entities.FirstOrDefault(x => x.Code.ToLowerInvariant().Equals(airportCode.ToLowerInvariant()));
    }
}
