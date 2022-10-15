#region Using Directives

using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross;
using MvvmCross.Commands;
using Nacelle.KMA.Core.Analytics;
using Nacelle.KMA.Core.Commands;
using Xamarin.Essentials;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public class ManageBookingViewModel: BaseClosableViewModel
    {
        #region Constructors

        public ManageBookingViewModel()
        {
            CallCommand = new MvxCommand(DoCallCommand);
            ManageBookingOnlineCommand = new TrackableAsyncCommand(Constants.Analytics.Events.ButtonTap, DoManageBookingOnline, Constants.Analytics.Target.ManageBookingsOnline);
            TelephoneNumber = Constants.KululaAlphaPhoneNo;
        }

        #endregion //Constructors

        #region Commands

        public MvxCommand CallCommand { get; }
        public ICommand ManageBookingOnlineCommand { get; }
        public string TelephoneNumber { get; }

        #endregion //Commands

        #region Commands Handlers

        private Task DoManageBookingOnline()
        {
            return Browser.OpenAsync(Constants.KululaManageBookingURL);
        }

        private void DoCallCommand()
        {
            try
            {
                Mvx.IoCProvider.Resolve<IAnalyticsService>().LogEvent(Constants.Analytics.Events.ButtonTap, Constants.Analytics.Target.Phone);
                PhoneDialer.Open(Constants.KululaPhoneNo);
            }
            catch
            {
                Debug.WriteLine("No phone on this device");
            }
        }

        #endregion //Commands Handlers
    }
}
