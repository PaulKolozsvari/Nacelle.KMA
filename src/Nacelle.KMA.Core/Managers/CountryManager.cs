using System.Collections.Generic;
using Nacelle.Core.Helpers;
using Nacelle.KMA.Core.Models.Entites;

namespace Nacelle.KMA.Core.Managers
{
    public class CountryManager : ICountryManager
    {
        public List<CountryEntity> GetCountryISOCodes()
        {
            var resourcePath = "Nacelle.KMA.Core.Resources.CountryISOCodes.json";
            var countries = ResourceHelper.GetResourceObject<List<CountryEntity>, CountryManager>(resourcePath);
            return countries;
        }

        public List<CountryEntity> GetCountryDialCodes()
        {
            var resourcePath = "Nacelle.KMA.Core.Resources.CountryDialCodes.json";
            var countries = ResourceHelper.GetResourceObject<List<CountryEntity>, CountryManager>(resourcePath);
            return countries;
        }
    }
}
