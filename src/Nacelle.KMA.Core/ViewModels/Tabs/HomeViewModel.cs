#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross;
using MvvmCross.Plugin.Messenger;
using Nacelle.KMA.API.Exceptions.Factories;
using Nacelle.KMA.Core.Builders;
using Nacelle.KMA.Core.Commands;
using Nacelle.KMA.Core.Managers;
using Nacelle.KMA.Core.Managers.Contracts;
using Nacelle.KMA.Core.Mappers;
using Nacelle.KMA.Core.Models.Items;
using Nacelle.KMA.Core.NavBundles;
using Nacelle.KMA.Core.Platform;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public class HomeViewModel : RefreshableViewModel
    {
        #region Constructors

        public HomeViewModel(
            IConnectivityManager connectivityManager,
            IToastService toastService,
            IBookingManager bookingManager,
            IMvxMessenger mvxMessenger) : base(connectivityManager, toastService, mvxMessenger)
        {
            BookingManager = bookingManager;
            FindBookingCommand = new TrackableAsyncCommand(Constants.Analytics.Events.ButtonTap, DoFindBookingCommandAsync, Constants.Analytics.Target.FindMyBooking);
            FlightExtrasCommand = new TrackableAsyncCommand(Constants.Analytics.Events.CardTap, DoFlightExtrasCommandAsync, Constants.Analytics.Target.ExtrasCard);
            BookFlightCommand = new TrackableAsyncCommand(Constants.Analytics.Events.ButtonTap, DoBookFlightCommandAsync, Constants.Analytics.Target.BookAFlight);
            CheckInCommand = new TrackableAsyncCommand<BookingItem>(Constants.Analytics.Events.ButtonTap, DoCheckInCommandAsync, Constants.Analytics.Target.CheckInNow, InitializeAnalyticsContext);
            ViewBoardingPassCommand = new TrackableAsyncCommand<BookingItem>(Constants.Analytics.Events.ButtonTap, DoViewBoardingPassCommandAsync, Constants.Analytics.Target.ViewBoardingPass, InitializeAnalyticsContext);
        }

        #endregion //Constructors

        #region Fields

        private IProgressActivityService _progressActivityService;

        #endregion //Fields

        #region Properties

        public IBookingManager BookingManager { get; }

        private IProgressActivityService ProgressActivityService        {            get            {                if (_progressActivityService == null)                {                    _progressActivityService = Mvx.IoCProvider.Resolve<IProgressActivityService>();                }                return _progressActivityService;            }        }

        #endregion //Properties

        #region Commands

        public ICommand FindBookingCommand { get; }
        public ICommand FlightExtrasCommand { get; }
        public ICommand BookFlightCommand { get; }
        public TrackableAsyncCommand<BookingItem> CheckInCommand { get; }
        public TrackableAsyncCommand<BookingItem> ViewBoardingPassCommand { get; }

        #endregion //Commands

        #region Command Handlers

        private Task DoBookFlightCommandAsync() =>
            NavigationService.Navigate<FlightBookingViewModel>();

        private Task DoFlightExtrasCommandAsync() =>
            NavigationService.Navigate<FlightExtrasViewModel>();

        private Task DoFindBookingCommandAsync() =>
            NavigationService.Navigate<FindBookingViewModel>();

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

        private async Task DoCheckInCommandAsync(BookingItem bookingItem)
        {
            if (ConnectivityManager.NetworkAccess == Enums.NetworkAccess.None)
            {
                await ToastService.Show(Constants.Messages.NoInternetConnectdion, true);
                return;
            }

            var progressActivityService = Mvx.IoCProvider.Resolve<IProgressActivityService>();

            progressActivityService.Show();

            try
            {
                // TODO: Rather use BookingItem commands to dry this up.
                var response = await BookingManager.FindBookingAsync(bookingItem.BookingReference, bookingItem.BookingLastName);
                if (response.IsSuccess)
                {
                    var checkinItems = response.Data.ToCheckInItemsEligible();
                    if (checkinItems != null && checkinItems.Any())
                    {
                        await NavigationService.Navigate<CheckInfoViewModel, CheckInNavBundle>(new CheckInNavBundle
                        {
                            BookingReference = bookingItem.BookingReference,
                            ConversationID = response.Data.ConversationID,
                            LastName = bookingItem.BookingLastName,
                            CheckInItems = checkinItems.ToList()
                        });
                    }
                    else
                    {
                        throw ExceptionFactory.CheckIn.NotEligible();
                    }
                }
            }
            catch (Exception ex)
            {
                await Mvx.IoCProvider.Resolve<IAlertService>().Show("", ex.Message, ("Ok", null));
            }
            finally
            {
                progressActivityService.Hide();
            }
        }

        //private async Task DoViewBoardingPassCommandAsync(BookingItem bookingItem)
        //{
        //    var response = await BookingManager.LoadBookingAsync(bookingItem.BookingReference);
        //    if (response != null)
        //    {
        //        var checkinItems = response.ToCheckInItemsEligible();
        //        await NavigationService.Navigate<BoardingPassViewModel, CheckedInNavBundle>(new CheckedInNavBundle
        //        {
        //            BookingReference = bookingItem.BookingReference,
        //            ConversationID = response.ConversationID,
        //            LastName = bookingItem.BookingLastName,
        //            CheckInItems = checkinItems.ToList()
        //        });
        //    }
        //}

        private async Task DoViewBoardingPassCommandAsync(BookingItem bookingItem)
        {
            if (ConnectivityManager.NetworkAccess == Nacelle.KMA.Core.Enums.NetworkAccess.None)            {                await ToastService.Show(Constants.Messages.NoInternetConnectdion, true);                return;            }

            ProgressActivityService.Show();
            try
            {
                var response = await BookingManager.FindBookingAsync(bookingItem.BookingReference, bookingItem.BookingLastName);
                if (response != null)
                {
                    var checkinItems = response.Data.ToCheckInItems(bookingItem.SegmentId);
                    await NavigationService.Navigate<BoardingPassViewModel, CheckedInNavBundle>(new CheckedInNavBundle
                    {
                        BookingReference = bookingItem.BookingReference,
                        ConversationID = response.Data.ConversationID,
                        LastName = bookingItem.BookingLastName,
                        CheckInItems = checkinItems.ToList()
                    });
                }
            }
            catch (Exception ex)            {                await Mvx.IoCProvider.Resolve<IAlertService>().Show("", ex.Message, ("Ok", null));            }            finally            {                ProgressActivityService.Hide();            }
        }

        #endregion //Command Handlers

        #region Methods

        public override async Task Initialize()
        {
            await base.Initialize();
            await BookingManager.PopulateBookingsAsync();
        }

        public override Task RefreshFunc()
        {
            if (BookingManager.BookingItems.Any())
            {
                return BookingManager.RefreshAllBookingsAsync();
            }
            return Task.Delay(1000);
        }

        #endregion //Methods
    }
}
