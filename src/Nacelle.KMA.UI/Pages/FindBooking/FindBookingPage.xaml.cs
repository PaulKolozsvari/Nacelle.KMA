using MvvmCross.Forms.Presenters.Attributes;
using Nacelle.KMA.Core.ViewModels;
using Nacelle.KMA.UI.Types;

namespace Nacelle.KMA.UI.Pages
{
    [MvxContentPagePresentation(WrapInNavigationPage = true, Title = "Find a Booking")]
    public partial class FindBookingPage : BaseContentPage<FindBookingViewModel>
    {
        public FindBookingPage()
        {
            InitializeComponent();
            SetPageStyle();
        }

        protected override StatusBarStyle PreferredStatusBarStyle() => StatusBarStyle.Light;
    }
}
