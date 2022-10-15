using MvvmCross.Forms.Presenters.Attributes;
using Nacelle.KMA.Core.ViewModels;

namespace Nacelle.KMA.UI.Pages
{
    [MvxContentPagePresentation(WrapInNavigationPage = true, Title = "Find a Booking")]
    public partial class FlightBookingPage : BaseContentPage<FlightBookingViewModel>
    {
        public FlightBookingPage()
        {
            InitializeComponent();
        }
    }
}
