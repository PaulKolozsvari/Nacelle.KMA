using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Nacelle.KMA.UI.Pages
{
    public class BaseTabbedPage<T> : MvxTabbedPage<T> where T : MvxViewModel
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();

            On<iOS>()
                .SetUseSafeArea(true)
                .SetLargeTitleDisplay(LargeTitleDisplayMode.Always);
        }
    }
}
