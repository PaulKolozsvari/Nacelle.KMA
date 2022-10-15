using System;
using System.Threading;
using Nacelle.KMA.API.Models.Enums;

namespace Nacelle.KMA.Core.ExtensionMethods
{
    public static class EnumExtensions
    {
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        public static PassengerTypes PassengerTypeFromString(this string value)
        {
            Enum.TryParse<PassengerTypes>(value, true, out var result);
            return result;
        }
    }
}
