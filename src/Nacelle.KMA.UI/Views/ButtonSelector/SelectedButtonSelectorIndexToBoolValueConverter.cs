using System;
using System.Globalization;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Views
{
    public class SelectedButtonSelectorIndexToBoolValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var selectedIndex = (int)value;

            if (selectedIndex == -1)
            {
                return false;
            }

            if (parameter is ButtonSelectorItemView buttonSelectorItemView)
            {
                return selectedIndex == buttonSelectorItemView.Index;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
