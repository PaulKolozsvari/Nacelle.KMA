using System;
namespace Nacelle.KMA.Core.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string value)
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
        }
    }
}
