using System;
using System.Collections;
using System.Globalization;
using MvvmCross.Forms.Converters;


namespace Nacelle.KMA.UI.Converters
{
    public class InverseHasItemsValueConverter: MvxFormsValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable list)
            {
                foreach(var item in list)
                {
                    return false;
                }

                return true;
            }

            // Not Enumrable, so return false;
            return false;
        }
    }


}
