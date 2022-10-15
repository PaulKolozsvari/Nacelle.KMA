using System;
using System.Globalization;
using MvvmCross.Forms.Converters;
namespace Nacelle.KMA.UI.Converters
{
    public class IsNullOrEmptyStringValueConverter: MvxFormsValueConverter<string ,bool>
    {
        protected override bool Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}
