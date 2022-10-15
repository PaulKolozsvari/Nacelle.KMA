using System;
using Nacelle.KMA.Core.Data;

namespace Nacelle.KMA.Core.ExtensionMethods
{
    public static class KeyLookups
    {
        public static string ToAirPortDescription(this string airportCode)
        {

            return DataFactory.AirportDictionary.ContainsKey(airportCode)
                 ? DataFactory.AirportDictionary[airportCode]
                 : "Unknown";
        }

        public static string ToCityDescription(this string cityCode)
        {

            return DataFactory.CityDictionary.ContainsKey(cityCode)
                 ? DataFactory.CityDictionary[cityCode]
                 : "Unknown";
        }
    }
}
