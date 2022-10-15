using System;
using System.Globalization;
using MvvmCross.Forms.Converters;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Converters
{
    public class TripCardIsExpandedPrimaryColorValueConverter : MvxFormsValueConverter<bool, Color>    
    {
        protected override Color Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value
                ? (Color)Application.Current.Resources["TripCardExpandedPrimaryColor"]
                : (Color)Application.Current.Resources["TripCardCollapsedPrimaryColor"];
        }
    }
}
