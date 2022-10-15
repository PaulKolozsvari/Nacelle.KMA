using System;
using System.Globalization;
using Nacelle.KMA.Core.Models.Enums;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Converters
{
    public class WeightCategoryToBoolValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WeightCategory weightCategory && parameter is WeightCategory weightCategoryParams)
            {
                return weightCategory == weightCategoryParams;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    public class InverseWeightCategoryToBoolValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WeightCategory weightCategory && parameter is WeightCategory weightCategoryParams)
            {
                return weightCategory != weightCategoryParams;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
