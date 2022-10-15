using System;
using System.Globalization;
using MvvmCross.Forms.Converters;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Converters
{
    public class TripCardIsExpandedSecondaryColorValueConverter : MvxFormsValueConverter<bool, Color>
    {
        protected override Color Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value
                ? (Color)Xamarin.Forms.Application.Current.Resources["TripCardExpandedSecondaryColor"]
                : (Color)Xamarin.Forms.Application.Current.Resources["TripCardCollapsedSecondaryColor"];
        }
    }
}
