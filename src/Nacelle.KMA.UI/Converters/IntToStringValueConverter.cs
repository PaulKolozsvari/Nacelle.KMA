using System;
using System.Globalization;
using MvvmCross.Forms.Converters;

namespace Nacelle.KMA.UI.Converters
{
    public class IntToStringValueConverter : MvxFormsValueConverter<int, string>
    {
        protected override string Convert(int value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == 0 ? string.Empty : value.ToString();
        }

        protected override int ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }

            if (int.TryParse(value, out var parsed))
            {
                return parsed;
            }

            return 0;
        }
    }
}
