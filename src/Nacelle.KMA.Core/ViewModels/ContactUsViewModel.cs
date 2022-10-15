#region Using Directives

using System.Diagnostics;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Commands;
using Nacelle.KMA.Core.Analytics;
using Xamarin.Essentials;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public class ContactUsViewModel: BaseClosableViewModel
    {
        #region Constructors

        public ContactUsViewModel()
        {
            CallCommand = new MvxCommand(DoCallCommand);
            EmailCommand = new MvxAsyncCommand(DoEmailCommand);
            TelephoneNumber = Constants.KululaAlphaPhoneNo;
            EmailAddress = Constants.KululaEmail;
        }

        #endregion //Constructors

        #region Commands

        public MvxCommand CallCommand { get; }
        public MvxAsyncCommand EmailCommand { get; }
        public string TelephoneNumber { get; }
        public string EmailAddress { get; }

        #endregion //Commands

        #region Command Handlers

        private async Task DoEmailCommand()
        {
            await Email.ComposeAsync("Feedback", string.Empty, Constants.KululaEmail);
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

        #endregion //Command Handlers
    }
}
