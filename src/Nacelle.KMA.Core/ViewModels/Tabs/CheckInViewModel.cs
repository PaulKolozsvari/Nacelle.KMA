#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.ViewModels;
using Nacelle.KMA.Core.Analytics;
using Nacelle.KMA.Core.Builders;
using Nacelle.KMA.Core.Commands;
using Nacelle.KMA.Core.Managers;
using Nacelle.KMA.Core.Managers.Contracts;
using Nacelle.KMA.Core.Mappers;
using Nacelle.KMA.Core.Models.Items;
using Nacelle.KMA.Core.NavBundles;
using Nacelle.KMA.Core.Platform;
using Nacelle.KMA.Core.Validators;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public class CheckInViewModel : BaseViewModel, IBookingQuery
    {
        public CheckInViewModel(
            IViewModelValidator viewModelValidator,
            IBookingManager bookingManager,
            IConnectivityManager connectivityManager,
            IToastService toastService,
            IProgressActivityService progressActivityService)
        {
            _bookingManager = bookingManager;
            _bookingManager.BookingItems.CollectionChanged += BookingItems_CollectionChanged;
            _viewModelValidator = viewModelValidator;
            _progressActivityService = progressActivityService;
            _connectivityManager = connectivityManager;
            _toastService = toastService;

            _validator = new CheckInValidator();

            BookingItems = new MvxObservableCollection<BookingItem>();
            CheckInCommand = new TrackableAsyncCommand(Constants.Analytics.Events.ButtonTap, DoCheckInCommandAsync, Constants.Analytics.Target.CheckIn);
            CheckInNowCommand = new TrackableAsyncCommand<BookingItem>(Constants.Analytics.Events.ButtonTap, DoCheckInNowCommandAsync, Constants.Analytics.Target.CheckInNow, InitializeAnalyticsContext);
        }

        #region Fields

        private readonly CheckInValidator _validator;
        private readonly IViewModelValidator _viewModelValidator;
        private readonly IProgressActivityService _progressActivityService;
        private readonly IConnectivityManager _connectivityManager;
        private readonly IToastService _toastService;
        private readonly IBookingManager _bookingManager;

        #endregion //Fields

        #region Methods

        public override async Task Initialize()
        {
            await base.Initialize();
            PopulateBookingItems();
        }

        private Dictionary<string, string> InitializeAnalyticsContext(BookingItem bookingItem)
        {
            return new AnalyticsContextBuilder().WithBookingReference(bookingItem.BookingReference)
                    .WithFlightNo(bookingItem.Number)
                    .WithOrigin(bookingItem.From)
                    .WithDestination(bookingItem.To)
                    .WithDepartureDate(bookingItem.DateFrom)
                    .WithDepartureTime(bookingItem.DateFrom)
                    .Build();
        }

        private void LogUserErrorEvent()
        {
            Mvx.IoCProvider.Resolve<IAnalyticsService>().LogEvent(Constants.Analytics.Events.FindBookinUserError, Constants.Analytics.Target.CheckIn);
        }

        #endregion //Methods

        #region Fields

        private string _bookingReference = string.Empty;
        private string _lastName;
        private string _errorMessage;

        #endregion //Fields

        #region Properties

        public MvxObservableCollection<BookingItem> BookingItems { get; }

        public string BookingReference
        {
            get => _bookingReference;
            set
            {
                if (SetProperty(ref _bookingReference, value.ToUpper()))
                {
                    ErrorMessage = string.Empty;
                }
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (SetProperty(ref _lastName, value))
                {
                    ErrorMessage = string.Empty;
                }
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        #endregion //Properties

        #region Methods

        private void PopulateBookingItems()
        {
            BookingItems.Clear();
            BookingItems.AddRange(_bookingManager.BookingItems.Where(x => x.IsCheckInVisible));
            RaisePropertyChanged(() => BookingItems);
        }

        #endregion //Methods

        #region Event Handlers

        private void BookingItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            PopulateBookingItems();
        }

        public override void ViewAppearing()
        {
            BookingReference = string.Empty;
            LastName = string.Empty;
            ErrorMessage = string.Empty;
            base.ViewAppearing();
        }

        #endregion //Event Handlers

        #region Commands

        public TrackableAsyncCommand CheckInCommand { get; }
        public TrackableAsyncCommand<BookingItem> CheckInNowCommand { get; }

        #endregion //Commands

        #region Command Handlers

        private async Task DoCheckInCommandAsync()
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
                    var response = await _bookingManager.FindAndSaveBookingAsync(BookingReference, LastName.Trim());
                    if (response.IsSuccess)
                    {
                        var checkinItems = response.Data.ToCheckInItemsEligible();
                        await NavigationService.Navigate<CheckInfoViewModel, CheckInNavBundle>(new CheckInNavBundle
                        {
                            ConversationID = response.Data.ConversationID,
                            BookingReference = BookingReference,
                            LastName = LastName,
                            CheckInItems = checkinItems.ToList()
                        });
                    }
                    else
                    {
                        LogUserErrorEvent();
                        ErrorMessage = response.Message;
                    }
                }
                catch (Exception ex)
                {
                    LogUserErrorEvent();
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
        }

        private async Task DoCheckInNowCommandAsync(BookingItem bookingItem)
        {
            try
            {
                // TODO
                _progressActivityService.Show();
                var response = await _bookingManager.FindAndSaveBookingAsync(BookingReference, LastName);
                if (response.IsSuccess)
                {
                    await NavigationService.Navigate<CheckInfoViewModel>();
                }
                else
                {
                    LogUserErrorEvent();
                    ErrorMessage = response.Message;
                }
            }
            catch (Exception ex)
            {
                LogUserErrorEvent();
                ErrorMessage = ex.Message;
            }
            finally
            {
                _progressActivityService.Hide();
            }
        }

        #endregion //Command Handlers


    }
}
