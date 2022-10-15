using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Forms.Presenters.Attributes;
using Nacelle.KMA.Core.ViewModels;
using Xamarin.Essentials;

namespace Nacelle.KMA.UI.Pages
{
    [MvxContentPagePresentation(WrapInNavigationPage = true, Title = "Contact Us")]
    public partial class ManageBookingPage : BaseContentPage<ManageBookingViewModel>
    {
        public ManageBookingPage()
        {
            InitializeComponent();

            ManageBookingOnlineCommand = new MvxAsyncCommand(DoManageBookingOnline);
        }

        private Task DoManageBookingOnline()
        {
            return Browser.OpenAsync(Core.Constants.KululaManageBookingURL, BrowserLaunchMode.SystemPreferred);
        }

        public MvxAsyncCommand ManageBookingOnlineCommand { get; }
    }
}
