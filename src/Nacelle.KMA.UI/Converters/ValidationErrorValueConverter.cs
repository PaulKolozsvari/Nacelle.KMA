using System;
using System.Collections.Generic;
using System.Globalization;
using MvvmCross.Forms.Converters;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Converters
{
    public class ValidationErrorColorValueConverter : MvxFormsValueConverter<Dictionary<string, string>, Color>
    {
        protected override Color Convert(Dictionary<string, string> value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && parameter != null && parameter is string key)
            {
                if (value.ContainsKey(key))
                {
                    return Color.Red;
                }
            }

            return Color.FromHex("#666666");
        }
    }

    public class ValidationErrorTextValueConverter : MvxFormsValueConverter<Dictionary<string, string>, string>
    {
        protected override string Convert(Dictionary<string, string> value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && parameter != null && parameter is string key)
            {
                if (value.ContainsKey(key))
                {
                    return value[key];
                }
            }

            return string.Empty;
        }
    }
}
