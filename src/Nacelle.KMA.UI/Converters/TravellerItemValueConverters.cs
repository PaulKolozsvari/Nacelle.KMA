using System;
using System.Globalization;
using MvvmCross.Forms.Converters;
using Nacelle.KMA.Core.Models.Items;

namespace Nacelle.KMA.UI.Converters
{
    public class TravellerItemToWeightCategoryVisibilityValueConverter : MvxFormsValueConverter<TravellerItem, bool>
    {
        protected override bool Convert(TravellerItem value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }

            if (value.IsInfant)
            {
                return false;
            }

            return !value.HasUpdatedWeightCategory;
        }
    }

    public class InverseTravellerItemToWeightCategoryVisibilityValueConverter : MvxFormsValueConverter<TravellerItem, bool>
    {
        protected override bool Convert(TravellerItem value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }

            if (value.IsInfant)
            {
                return false;
            }

            return value.HasUpdatedWeightCategory;
        }
    }
}
