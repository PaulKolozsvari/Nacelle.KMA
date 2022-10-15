using MvvmCross.Forms.Presenters.Attributes;
using Nacelle.KMA.Core.ViewModels;
using Nacelle.KMA.UI.Types;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Nacelle.KMA.UI.Pages
{
    [MvxTabbedPagePresentation(WrapInNavigationPage = false)]
    public partial class MenuPage : BaseContentPage<MenuViewModel>
    {
        public MenuPage()
        {
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

            SetPageStyle();
        }

        protected override StatusBarStyle PreferredStatusBarStyle() => StatusBarStyle.Light;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            On<iOS>().SetUseSafeArea(true);
        }
    }
}
