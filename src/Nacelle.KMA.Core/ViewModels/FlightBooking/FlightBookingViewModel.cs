#region Using Directives

using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using Nacelle.KMA.API;
using Nacelle.KMA.Core.Commands;
using Xamarin.Essentials;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public class FlightBookingViewModel: BaseClosableViewModel
    {
        #region Constructors

        public FlightBookingViewModel()
        {
            ContinueCommand = new TrackableAsyncCommand(Constants.Analytics.Events.ButtonTap, DoContinueCommandAsync, Constants.Analytics.Target.Continue);
            CancelCommand = new TrackableAsyncCommand(Constants.Analytics.Events.ButtonTap, DoCancelCommandAsync, Constants.Analytics.Target.NoThanks);
        }

        #endregion //Constructors

        #region Commands

        public ICommand ContinueCommand { get; }
        public ICommand CancelCommand { get; }

        #endregion //Commands

        #region Command Handlers

        private Task DoCancelCommandAsync()
        {
            return NavigationService.Close(this);
        }

        private Task DoContinueCommandAsync()
        {
            return Browser.OpenAsync(Constants.KululaURL, BrowserLaunchMode.SystemPreferred);
        }

        #endregion //Command Handlers
    }
}
