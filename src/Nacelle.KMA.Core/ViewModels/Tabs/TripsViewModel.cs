#region Using Directives

using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Nacelle.KMA.Core.Managers;
using Nacelle.KMA.Core.Models.Items;
using MvvmCross;
using Nacelle.KMA.Core.Platform;
using Nacelle.KMA.Core.Analytics;
using Nacelle.KMA.Core.Builders;
using Nacelle.KMA.Core.Managers.Contracts;
using System.Linq;
using MvvmCross.Plugin.Messenger;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public class TripsViewModel : RefreshableViewModel
    {
        #region Constructors

        public TripsViewModel(
            IBookingManager bookingManager,
            IConnectivityManager connectivityManager,
            IToastService toastService,
            IMvxMessenger mvxMessenger) : base(connectivityManager, toastService, mvxMessenger)
        {
            BookingManager = bookingManager;

            TripItemMenuCommand = new MvxCommand<TripItem>(DoTripItemMenuCommand);
            MainMenuCommand = new MvxCommand<string>(DoMainMenuCommand);
            FindBookingCommand = new MvxCommand(() => NavigationService.Navigate<FindBookingViewModel>());
            BookFlightCommand = new MvxCommand(() => NavigationService.Navigate<FlightBookingViewModel>());
            FlightDetailsCommand = new MvxAsyncCommand<BookingItem>(DoFlightDetailsCommandAsync);
            TripItems = new MvxObservableCollection<ITripItem>();
            TripMenuItems = new[]
            {
                Constants.ContextActions.ManageBooking,
                Constants.ContextActions.AddExtras,
                Constants.ContextActions.RemoveBooking
            };
            MainMenuItems = new[]
            {
                Constants.ContextActions.BookAFlight,
                Constants.ContextActions.FindABooking,
            };
        }

        #endregion //Constructors

        #region Properties

        public IBookingManager BookingManager { get; }
        public MvxObservableCollection<ITripItem> TripItems { get; }
        public string[] TripMenuItems { get; }
        public string[] MainMenuItems { get; }

        #endregion //Properties

        #region Commands

        public MvxAsyncCommand<BookingItem> FlightDetailsCommand { get; }
        public MvxCommand<TripItem> TripItemMenuCommand { get; }
        public MvxCommand<string> MainMenuCommand { get; }
        public MvxCommand FindBookingCommand { get; }
        public MvxCommand BookFlightCommand { get; }

        #endregion //Commands

        #region Command Handlers

        private void DoTripItemMenuCommand(TripItem tripItem)
        {
            switch (tripItem.SelectedMenu)
            {
                case Constants.ContextActions.ManageBooking:
                    LogEvent(Constants.Analytics.Target.ManageBooking, tripItem.BookingReference);
                    NavigationService.Navigate<ManageBookingViewModel>();
                    break;
                case Constants.ContextActions.AddExtras:
                    LogEvent(Constants.Analytics.Target.AddExtras, tripItem.BookingReference);
                    NavigationService.Navigate<FlightExtrasViewModel>();
                    break;
                case Constants.ContextActions.RemoveBooking:
                    LogEvent(Constants.Analytics.Target.RemoveFromApp, tripItem.BookingReference);
                    MvxNotifyTask.Create(DoRemoveBookingAsync(tripItem));
                    break;
            }
        }

        private void LogEvent(string target, string bookingReference)
        {
            Mvx.IoCProvider.Resolve<IAnalyticsService>().LogEvent(Constants.Analytics.Events.OptionsItemTap, target, new AnalyticsContextBuilder().WithBookingReference(bookingReference).Build());
        }

        private void DoMainMenuCommand(string menu)
        {
            switch (menu)
            {
                case Constants.ContextActions.BookAFlight:
                    NavigationService.Navigate<FlightBookingViewModel>();
                    break;
                case Constants.ContextActions.FindABooking:
                    NavigationService.Navigate<FindBookingViewModel>();
                    break;
            }
        }

        private async Task DoRemoveBookingAsync(TripItem obj)
        {
            var alert = Mvx.IoCProvider.Resolve<IAlertService>();

            var yes = (Title: "Keep", Action: new Func<Task>(() => Task.FromResult(false)));
            var no = (Title: "Remove", Action: new Func<Task>(() => BookingManager.RemoveBooking(obj.BookingReference)));

            await alert.Show("Remove trip from app?",
                "Your trip will be removed from this app.\nThis will not cancel your booking.",
                yes,
                no);
        }

        private Task DoFlightDetailsCommandAsync(BookingItem arg)
        {
            return NavigationService.Navigate<FlightDetailViewModel, BookingItem>(arg);
        }

        #endregion //Command Handlers

        #region Methods

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