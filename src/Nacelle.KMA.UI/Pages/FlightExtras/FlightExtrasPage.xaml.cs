using MvvmCross.Forms.Presenters.Attributes;
using Nacelle.KMA.Core.ViewModels;
using Nacelle.KMA.UI.Types;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Nacelle.KMA.UI.Pages
{
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    public partial class FlightExtrasPage : BaseContentPage<FlightExtrasViewModel>
    {
        public FlightExtrasPage()
        {
            InitializeComponent();
        }

        protected override StatusBarStyle PreferredStatusBarStyle() => StatusBarStyle.Light;
    }
}
