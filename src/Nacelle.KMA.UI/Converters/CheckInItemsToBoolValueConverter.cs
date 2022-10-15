using System;
using System.Collections.Generic;
using System.Globalization;
using Nacelle.KMA.Core.Models.Items;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Converters
{
    public class CheckInItemsToBoolValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<CheckInItem> checkinItems)
            {
                return checkinItems.Count > 1;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
