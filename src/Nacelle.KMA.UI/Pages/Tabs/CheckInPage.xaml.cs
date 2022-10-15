using MvvmCross.Forms.Presenters.Attributes;
using Nacelle.KMA.Core.ViewModels;
using Nacelle.KMA.UI.Types;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Nacelle.KMA.UI.Pages
{
    [MvxTabbedPagePresentation(WrapInNavigationPage = false)]
    public partial class CheckInPage : BaseContentPage<CheckInViewModel>
    {
        public CheckInPage()
        {
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
        }

        protected override StatusBarStyle PreferredStatusBarStyle() => StatusBarStyle.Light;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            On<iOS>().SetUseSafeArea(true);
        }
    }
}
