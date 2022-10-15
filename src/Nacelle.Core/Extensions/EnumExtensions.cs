using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Xml.Serialization;

namespace Nacelle.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string ToStringValue(this Enum enumType)
        {
            return enumType
                .GetType()
                .GetRuntimeFields()
                .Skip(1).FirstOrDefault(x => x.Name == enumType.ToString())
                .GetCustomAttribute<XmlAttributeAttribute>().AttributeName;
        }

        /*  
            Commenting this out until BuildServer supports Mono 6.0
            Type Constraint T : Enum not supported on BuildServer yet (need Mono 6.0)
        */

        /// <summary>
        /// Case Insensitive Parse of Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        //public static T ParseEnumFromString<T>(this string enumValue) where T : Enum
        //{
        //    if (string.IsNullOrWhiteSpace(enumValue))
        //    {
        //        return default(T);
        //    }

        //    var result =
        //        typeof(T)
        //        .GetRuntimeFields()
        //        .Skip(1)
        //        .Select(x => (T)x.GetRawConstantValue())
        //        .FirstOrDefault(x => x.ToString().ToLower().Equals(enumValue.ToLower()));

        //    return result;
        //}

        //public static T ParseEnumFromAttributeValue<T>(this string enumAttributeValue) where T: Enum
        //{
        //    var stringValues =
        //        typeof(T)
        //        .GetRuntimeFields()
        //        .Skip(1)
        //        .Select(x => x.GetCustomAttribute<XmlAttributeAttribute>().AttributeName)
        //        .ToList();

        //    var enumIndex = stringValues.IndexOf(enumAttributeValue);
        //    var enumValues = Enum.GetValues(typeof(T)).OfType<T>().ToArray();
        //    var enumResult = enumValues[enumIndex];
        //    return enumResult;
        //}
    }
}
