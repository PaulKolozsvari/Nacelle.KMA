using System;

using Xamarin.Forms;
using MvvmCross.Forms.Converters;
using System.Globalization;

namespace Nacelle.KMA.UI.Converters
{
    public class ColorToStringValueConverter : MvxFormsValueConverter<Color, string>
    {
        protected override string Convert(Color value, Type targetType, object parameter, CultureInfo culture)
        {
            var red = (int)value.R;
            var green = (int)value.G;
            var blue = (int)value.B;
            return red.ToString("X2") + green.ToString("X2") + blue.ToString("X2");
        }
    }
}

