using System.Threading.Tasks;
using Nacelle.KMA.Core.Models.Entites;

namespace Nacelle.KMA.Core.Managers.Contracts
{
    public interface IAirportManager
    {
        Task<AirportEntity> GetAirport(string airportCode, bool forceRefresh = false);
    }
}
