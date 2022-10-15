using System.Collections.Generic;
using System.Threading.Tasks;
using Nacelle.KMA.Core.Models.Entites;

namespace Nacelle.KMA.Core.Managers.Contracts
{
    public interface IWeatherManager
    {
        Task<List<WeatherEntity>> GetOrFetchWeatherData(string airportCode, bool forceRefresh = false);
    }
}
