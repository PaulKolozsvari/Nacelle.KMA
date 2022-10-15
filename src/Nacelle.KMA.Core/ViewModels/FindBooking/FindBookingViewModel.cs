#region Using Directives

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross;
using MvvmCross.Plugin.Messenger;
using Nacelle.KMA.Core.Analytics;
using Nacelle.KMA.Core.Commands;
using Nacelle.KMA.Core.Managers;
using Nacelle.KMA.Core.Managers.Contracts;
using Nacelle.KMA.Core.Models.Messages;
using Nacelle.KMA.Core.Platform;
using Nacelle.KMA.Core.Validators;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public class FindBookingViewModel : BaseClosableViewModel, IBookingQuery
    {
        #region Constructors

        public FindBookingViewModel(
            IMvxMessenger mvxMessenger,
            IViewModelValidator viewModelValidator,
            IBookingManager bookingManager,
            IConnectivityManager connectivityManager,
            IToastService toastService,
            IProgressActivityService progressActivityService)
        {
            FindBookingCommand = new TrackableAsyncCommand(Constants.Analytics.Events.ButtonTap, DoFindBookingCommandAsync, Constants.Analytics.Target.FindABooking);
            _validator = new FindBookingValidator();
            _mvxMessenger = mvxMessenger;
            _viewModelValidator = viewModelValidator;
            _bookingManager = bookingManager;
            _connectivityManager = connectivityManager;
            _toastService = toastService;
            _progressActivityService = progressActivityService;
        }

        #endregion //Constructors

        #region Fields

        private readonly FindBookingValidator _validator;
        private readonly IMvxMessenger _mvxMessenger;
        private readonly IViewModelValidator _viewModelValidator;
        private readonly IBookingManager _bookingManager;
        private readonly IConnectivityManager _connectivityManager;
        private readonly IToastService _toastService;
        private readonly IProgressActivityService _progressActivityService;
        private string _errorMessage;

        #endregion //Fields

        #region Properties

        public string BookingReference { get; set; }
        public string LastName { get; set; }
        public string ErrorMessage { get => _errorMessage; set => SetProperty(ref _errorMessage, value); }

        #endregion //Properties

        #region Commands

        public ICommand FindBookingCommand { get; }

        #endregion //Commands

        #region Command Handlers

        private async Task DoFindBookingCommandAsync()
        {
            var isValid = _viewModelValidator.Validate(_validator, this);
            if (isValid)
            {
                if (_connectivityManager.NetworkAccess == Enums.NetworkAccess.None)
                {
                    await _toastService.Show(Constants.Messages.NoInternetConnectdion, true);
                    return;
                }
                _progressActivityService.Show();
                try
                {
                    var response = await _bookingManager.QueryBookingAsync(BookingReference, LastName.Trim()).ConfigureAwait(false);
                    if (response.IsSuccess)
                    {
                        _mvxMessenger.Publish(new ShowViewModelMessage(this, typeof(TripsViewModel)));
                        await NavigationService.Close(this);
                    }
                    else
                    {
                        Mvx.IoCProvider.Resolve<IAnalyticsService>().LogEvent(Constants.Analytics.Events.FindBookinUserError, Constants.Analytics.Target.FindABooking);
                        ErrorMessage = response.Message;
                    }
                }
                catch (Exception ex)
                {
                    Mvx.IoCProvider.Resolve<IAnalyticsService>().LogEvent(Constants.Analytics.Events.FindBookinUserError, Constants.Analytics.Target.FindABooking);
                    ErrorMessage = ex.Message;
                }
                finally
                {
                    _progressActivityService.Hide();
                }
            }
            else
            {
                ErrorMessage = _viewModelValidator.ErrorMessages.FirstOrDefault();
            }
            await Task.FromResult(true);
        }

        #endregion //Command Handlers
    }
}
