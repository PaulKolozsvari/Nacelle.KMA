using System.Collections.Generic;
using Nacelle.KMA.Core.Models.Entites;

namespace Nacelle.KMA.Core.Managers
{
    public interface ICountryManager
    {
        List<CountryEntity> GetCountryISOCodes();
        List<CountryEntity> GetCountryDialCodes();
    }
}
