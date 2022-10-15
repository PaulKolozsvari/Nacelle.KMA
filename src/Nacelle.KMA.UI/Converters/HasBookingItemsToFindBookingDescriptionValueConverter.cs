using System;
using System.Collections;
using System.Globalization;
using MvvmCross.Forms.Converters;

namespace Nacelle.KMA.UI.Converters
{
    public class HasBookingItemsToFindBookingDescriptionValueConverter : MvxFormsValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hasItems = false;
            if (value is IEnumerable list)
            {
                foreach (var item in list)
                {
                    hasItems = true;
                }
            }

            return hasItems
                ? "Check-in for a different flight?"
                : "Which flight would you like to check-in for?";
        }
    }
}
