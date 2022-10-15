using Nacelle.KMA.Core.Models.Items;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Templates
{
    public class MenuItemDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MenuItemTemplate { get; set; }
        public DataTemplate SeperatorTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return item is MenuItemSeparator ? SeperatorTemplate : MenuItemTemplate;
        }
    }
}
