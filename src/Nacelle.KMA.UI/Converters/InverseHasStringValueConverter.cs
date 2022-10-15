using System;
using System.Globalization;
using MvvmCross.Forms.Converters;

namespace Nacelle.KMA.UI.Converters
{
    public class InverseHasStringValueConverter : MvxFormsValueConverter<string, bool>
    {
        protected override bool Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}
