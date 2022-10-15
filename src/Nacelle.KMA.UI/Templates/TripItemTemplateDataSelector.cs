using Xamarin.Forms;
using Nacelle.KMA.Core.Models.Items;
using System;

namespace Nacelle.KMA.UI.Templates
{
    public class TripItemTemplateDataSelector : DataTemplateSelector
    {
        public DataTemplate TripItemTemplate { get; set; }
        public DataTemplate TripDateSectionHeaderTemplate { get; set; }
        public DataTemplate PastTripsSectionHeaderTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            switch (item)
            {
                case TripItem _:
                    return TripItemTemplate;
                case TripDateSectionHeaderItem _:
                    return TripDateSectionHeaderTemplate;
                case PastTripsSectionHeaderItem _:
                    return PastTripsSectionHeaderTemplate;
                default:
                    throw new Exception($"TripItemTemplateDataSelector - No template defined for {item.GetType().Name}");
            }
        }
    }
}
