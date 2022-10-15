using System;
using System.Globalization;
using MvvmCross.Forms.Converters;

namespace Nacelle.KMA.UI.Converters
{
    public class StringToUpperCaseValueConverter : MvxFormsValueConverter<string, string>
    {
        protected override string Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is string source
                ? source.ToUpperInvariant()
                : string.Empty;
        }
    }
}
