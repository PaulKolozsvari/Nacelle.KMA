#region Using Directives

using System;using System.Collections.Generic;using System.Linq;using System.Threading.Tasks;using System.Windows.Input;using MvvmCross;using Nacelle.KMA.API.Exceptions.Factories;
using Nacelle.KMA.Core.Builders;using Nacelle.KMA.Core.Commands;using Nacelle.KMA.Core.ExtensionMethods;using Nacelle.KMA.Core.Managers;using Nacelle.KMA.Core.Managers.Contracts;using Nacelle.KMA.Core.Mappers;using Nacelle.KMA.Core.NavBundles;using Nacelle.KMA.Core.Platform;using Nacelle.KMA.Core.ViewModels;


#endregion //Using Directives
namespace Nacelle.KMA.Core.Models.Items{    public class BookingItem : NavigationItem    {
        #region Constructors

        public BookingItem()        {            CheckInCommand = new TrackableAsyncCommand(Constants.Analytics.Events.ButtonTap, DoCheckInCommandAsync, Constants.Analytics.Target.CheckInNow, InitializeAnalyticsContext);            ViewBoardingPassCommand = new TrackableAsyncCommand(Constants.Analytics.Events.ButtonTap, DoViewBoardingPassCommandAsync, Constants.Analytics.Target.ViewBoardingPass, InitializeAnalyticsContext);        }

        #endregion //Constructors

        #region Fields

        private IBookingManager _bookingManager;        private IProgressActivityService _progressActivityService;        private IConnectivityManager _connectivityManager;        private IToastService _toastService;

        #endregion //Fields

        #region Properties

        public string Carrier { get; set; }        public string From { get; set; }        public string To { get; set; }        public string Number { get; set; }        public DateTime DateFrom { get; set; }        public DateTime DateTo { get; set; }        public DateTime BoardingTime { get; set; }        public string SegmentId { get; set; }        public string BookingReference { get; set; }        public string BookingLastName { get; set; }        public string ConversationID { get; set; }        public string PassengerFlightId { get; set; }        public string Gate { get; set; }        public string Terminal { get; set; }        public string FlightNumber { get; set; }        public string FlightDateTimeSpan =>            DateFrom.ToString("dd MMM") + ", " +            DateFrom.ToString("HH:mm") + " - " +            DateTo.ToString("HH:mm");        public string Caption => $"{From.ToAirPortDescription()} to {To.ToAirPortDescription()}";        public string FromCity => From.ToCityDescription();        public string ToCity => To.ToCityDescription();

        // TODO: Should be using API to checkin-in is available and not rely on a client side time calc.
        public bool IsCheckInVisible => CanCheckIn && !IsViewBoardingPassVisible;        public bool IsViewBoardingPassVisible { get; set; }        public bool IsCheckInOrBoardingPassVisible => CanCheckIn || IsViewBoardingPassVisible;        public bool HasCheckedIn { get; set; }
        public bool IsKululaFlight => Carrier.Equals("MN", StringComparison.OrdinalIgnoreCase);

        public string FullName { get; internal set; }        public string Seat { get; internal set; }        public bool CanCheckIn { get; internal set; }        public bool HasQJump { get; internal set; }

        public IConnectivityManager ConnectivityManager        {            get            {                _connectivityManager = _connectivityManager ?? Mvx.IoCProvider.Resolve<IConnectivityManager>();                return _connectivityManager;            }        }        public IToastService ToastService        {            get            {                _toastService = _toastService ?? Mvx.IoCProvider.Resolve<IToastService>();                return _toastService;            }        }

        private IBookingManager BookingManager
        {
            get
            {                if (_bookingManager == null)                {                    _bookingManager = Mvx.IoCProvider.Resolve<IBookingManager>();                }                return _bookingManager;            }
        }        private IProgressActivityService ProgressActivityService        {            get            {                if (_progressActivityService == null)                {                    _progressActivityService = Mvx.IoCProvider.Resolve<IProgressActivityService>();                }                return _progressActivityService;            }        }

        #endregion //Properties

        #region Methods

        protected Dictionary<string, string> InitializeAnalyticsContext()        {            return new AnalyticsContextBuilder().WithBookingReference(this.BookingReference)                .WithFlightNo(this.Number)                .WithOrigin(this.From)                .WithDestination(this.To)                .WithDepartureDate(this.DateFrom)                .WithDepartureTime(this.DateFrom)                .Build();        }

        #endregion //Methods

        #region Commands

        public virtual ICommand FlightDetailCommand => new TrackableAsyncCommand(Constants.Analytics.Events.CardTap, DoFlightDetailCommandAsync, Constants.Analytics.Target.FlightCard, InitializeAnalyticsContext);        public TrackableAsyncCommand CheckInCommand { get; }        public TrackableAsyncCommand ViewBoardingPassCommand { get; }

        #endregion //Commands

        #region Command Handlers

        protected virtual Task DoFlightDetailCommandAsync()        {            return NavigationService.Navigate<FlightDetailViewModel, BookingItem>(this);        }

        private async Task DoCheckInCommandAsync()        {            if (ConnectivityManager.NetworkAccess == Nacelle.KMA.Core.Enums.NetworkAccess.None)            {                await ToastService.Show(Constants.Messages.NoInternetConnectdion, true);                return;            }            ProgressActivityService.Show();            try            {                var response = await BookingManager.FindBookingAsync(BookingReference, BookingLastName);
                if (response.IsSuccess)
                {
                    this.ConversationID = response.Data.ConversationID;
                    var checkinItems = response.Data.ToCheckInItemsEligible();
                    if (checkinItems != null && checkinItems.Any())
                    {
                        await NavigationService.Navigate<CheckInfoViewModel, CheckInNavBundle>(new CheckInNavBundle
                        {
                            BookingReference = this.BookingReference,
                            ConversationID = this.ConversationID,
                            LastName = this.BookingLastName,
                            CheckInItems = checkinItems.ToList()
                        });
                    }
                    else
                    {
                        throw ExceptionFactory.CheckIn.NotEligible();
                    }
                }
            }            catch (Exception ex)            {                await Mvx.IoCProvider.Resolve<IAlertService>().Show("", ex.Message, ("Ok", null));            }            finally            {                ProgressActivityService.Hide();            }        }        private async Task DoViewBoardingPassCommandAsync()        {
            if (ConnectivityManager.NetworkAccess == Nacelle.KMA.Core.Enums.NetworkAccess.None)            {                await ToastService.Show(Constants.Messages.NoInternetConnectdion, true);                return;            }
            ProgressActivityService.Show();            try            {
                string segment = this.SegmentId;
                var response = await BookingManager.FindBookingAsync(BookingReference, BookingLastName);
                if (response != null && response.IsSuccess) //Boarding pass code.
                {
                    this.ConversationID = response.Data.ConversationID;
                    var checkinItems = response.Data.ToCheckInItems(SegmentId);
                    await NavigationService.Navigate<BoardingPassViewModel, CheckedInNavBundle>(new CheckedInNavBundle
                    {
                        BookingReference = BookingReference,
                        LastName = BookingLastName,
                        ConversationID = this.ConversationID,
                        CheckInItems = checkinItems.ToList()
                    });
                }
            }            catch (Exception ex)            {                await Mvx.IoCProvider.Resolve<IAlertService>().Show("", ex.Message, ("Ok", null));            }            finally            {                ProgressActivityService.Hide();            }        }

        #endregion //Command Handlers    }}