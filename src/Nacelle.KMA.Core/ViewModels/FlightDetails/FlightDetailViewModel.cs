#region Using Directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using Nacelle.Core.Helpers;
using Nacelle.Core.Models;
using Nacelle.KMA.API.Models.Responses;
using Nacelle.KMA.Core.Analytics;
using Nacelle.KMA.Core.Builders;
using Nacelle.KMA.Core.Commands;
using Nacelle.KMA.Core.Enums;
using Nacelle.KMA.Core.ExtensionMethods;
using Nacelle.KMA.Core.Managers;
using Nacelle.KMA.Core.Managers.Contracts;
using Nacelle.KMA.Core.Mappers;
using Nacelle.KMA.Core.Models.Entites;
using Nacelle.KMA.Core.Models.Items;
using Nacelle.KMA.Core.NavBundles;
using Nacelle.KMA.Core.Platform;
using Nacelle.KMA.Core.ViewModels.FlightDetails;
using Xamarin.Essentials;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public class FlightDetailViewModel : RefreshableAndClosableViewModel,
        IMvxViewModel<BookingItem>,
        IMvxViewModel<TripItem>
    {
        #region Constructors

        public FlightDetailViewModel(
            IWeatherManager weatherManager,
            IBookingManager bookingManager,
            IProgressActivityService progressActivityService,
            IConnectivityManager connectivityManager,
            IToastService toastService,
            IMvxMessenger mvxMessenger) : base(connectivityManager, toastService, mvxMessenger)
        {
            _bookingManager = bookingManager;
            _weatherManager = weatherManager;
            _progressActivityService = progressActivityService;
            ShareFlightCommand = new TrackableAsyncCommand(Constants.Analytics.Events.BoardingPassShare, DoShareFlightCommandAsync, context: InitializeBookingReferenceAnalyticsContext);
            CheckInCommand = new TrackableAsyncCommand<BookingItem>(Constants.Analytics.Events.ButtonTap, DoCheckInCommandAsync, Constants.Analytics.Target.CheckInNow, InitializeAnalyticsContextBookingItem);
            ViewBoardingPassCommandBookingItem = new TrackableAsyncCommand<BookingItem>(Constants.Analytics.Events.ButtonTap, DoViewBoardingPassCommandAsync, Constants.Analytics.Target.ViewBoardingPass, InitializeAnalyticsContextBookingItem);
            ViewBoardingPassCommandTravellerItem = new TrackableAsyncCommand<TravellerItem>(Constants.Analytics.Events.ButtonTap, DoViewBoardingPassCommandAsyncTravellerItem, Constants.Analytics.Target.ViewBoardingPass, InitializeAnalyticsContextTravellerItem);
            TripItemMenuCommand = new MvxCommand<TripItem>(DoTripItemMenuCommand);
            NavMenuCommand = new MvxCommand<string>(DoNavMenuCommand);
            TripMenuItems = new[]
            {
                Constants.ContextActions.ManageBooking,
                Constants.ContextActions.AddExtras,
                Constants.ContextActions.RemoveBooking
            };
            TravellerItems = new List<TravellerItem>();
            BookingItems = new List<BookingItem>();
        }

        #endregion //Constructors

        #region Fields

        private string Caption { get; set; }
        private string FullFlightNumber { get; set; }
        private string FlightNo { get; set; }
        public string BookingLastName { get; private set; }
        private string BookingReference { get; set; }
        private readonly IWeatherManager _weatherManager;
        private readonly List<IDisposable> _observers = new List<IDisposable>();
        private IProgressActivityService _progressActivityService;
        private IBookingManager _bookingManager;

        private WeatherItem _weather;
        private string _checkInMessage;
        private string _checkInDateTime;
        private string _formattedCheckInTime;
        private string _title;
        private string _from;
        private string _to;
        private DateTime _dateFrom;
        private DateTime _dateTo;
        private DateTime BoardingTime { get; set; }
        private string _gate;
        private string _terminal;

        private bool _hasCheckedIn;
        private bool _canCheckIn;

        private FlightDetailCardsVisibility _cardsVisibility;

        #endregion //Fields

        #region Properties

        public bool IsFutureBooking { get; set; }

        public string[] TripMenuItems { get; }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string From
        {
            get => _from;
            set => SetProperty(ref _from, value);
        }

        public string To
        {
            get => _to;
            set => SetProperty(ref _to, value);
        }

        public DateTime DateFrom
        {
            get => _dateFrom;
            set => SetProperty(ref _dateFrom, value);
        }

        public DateTime DateTo
        {
            get => _dateTo;
            set => SetProperty(ref _dateTo, value);
        }

        public WeatherItem Weather
        {
            get => _weather;
            set => SetProperty(ref _weather, value);
        }

        public string CheckInMessage
        {
            get => _checkInMessage;
            set => SetProperty(ref _checkInMessage, value);
        }

        public string CheckInDateTime
        {
            get => _checkInDateTime;
            set => SetProperty(ref _checkInDateTime, value);
        }

        public bool HasCheckedIn
        {
            get => _hasCheckedIn;
            set => SetProperty(ref _hasCheckedIn, value);
        }

        public bool CanCheckIn
        {
            get => _canCheckIn;
            set => SetProperty(ref _canCheckIn, value);
        }

        public string FullName { get; private set; }

        public BookingItem BookingItem { get; private set; }

        public string Terminal
        {
            get => _terminal;
            set => SetProperty(ref _terminal, value);
        }

        public string Gate
        {
            get => _gate;
            set => SetProperty(ref _gate, value);
        }
        public string Seat { get; private set; }

        public string FormattedCheckInTime
        {
            get => _formattedCheckInTime;
            set => SetProperty(ref _formattedCheckInTime, value);
        }

        public FlightDetailCardsVisibility CardsVisibility
        {
            get => _cardsVisibility;
            set => SetProperty(ref _cardsVisibility, value);
        }

        private IBookingManager BookingManager
        {
            get
            {                if (_bookingManager == null)                {                    _bookingManager = Mvx.IoCProvider.Resolve<IBookingManager>();                }                return _bookingManager;            }
        }

        private IProgressActivityService ProgressActivityService        {            get            {                if (_progressActivityService == null)                {                    _progressActivityService = Mvx.IoCProvider.Resolve<IProgressActivityService>();                }                return _progressActivityService;            }        }

        public string ConversationID { get; set; }

        public string SegmentId { get; set; }

        #endregion //Properties

        #region Commands

        public TrackableAsyncCommand ShareFlightCommand { get; }
        public TrackableAsyncCommand<BookingItem> CheckInCommand { get; }
        public TrackableAsyncCommand<BookingItem> ViewBoardingPassCommandBookingItem { get; }
        public TrackableAsyncCommand<TravellerItem> ViewBoardingPassCommandTravellerItem { get; }
        public TripItem TripItem { get; set; }
        public List<CheckInItem> CheckInItems { get; set; }
        public List<TravellerItem> TravellerItems { get; set; }
        public List<BookingItem> BookingItems { get; set; }
        
        public MvxAsyncCommand<string> FlightItemCommand => new MvxAsyncCommand<string>(DoFlightItemAsync);
        public MvxCommand<TripItem> TripItemMenuCommand { get; }
        public MvxCommand<string> NavMenuCommand { get; }
        public Action ScrollToTop;

        #endregion //Commands

        #region Methods

        #region View Model Callbacks

        public async void Prepare(BookingItem parameter)
        {
            Title = $"Flight: {parameter.Number}";
            To = parameter.To;
            From = parameter.From;
            DateFrom = parameter.DateFrom;
            DateTo = parameter.DateTo;
            Caption = parameter.Caption;
            FullFlightNumber = parameter.Number;
            FlightNo = parameter.FlightNumber;
            BookingLastName = parameter.BookingLastName;
            BookingReference = parameter.BookingReference;
            BoardingTime = parameter.BoardingTime;
            Terminal = parameter.Terminal;
            Gate = parameter.Gate;
            Seat = parameter.Seat;
            HasCheckedIn = parameter.HasCheckedIn;
            CanCheckIn = parameter.CanCheckIn;
            FullName = parameter.FullName;
            IsFutureBooking = parameter.DateFrom.IsFutureBooking();
            BookingItem = parameter;
            SegmentId = parameter.SegmentId;
            BookingEntity bookingEntity = await _bookingManager.LoadBookingAsync(parameter.BookingReference);
            if (bookingEntity != null)
            {
                CheckInItems = bookingEntity.ToCheckInItems(parameter.SegmentId, parameter).ToList();
                TravellerItems.Clear();
                BookingItems.Clear();
                foreach (CheckInItem checkInItem in CheckInItems)
                {
                    TravellerItems.AddRange(checkInItem.TravellerItems);
                    BookingItems.AddRange(checkInItem.BookingItems);
                }
            }
            if (TripItem == null)
            {
                var tripItems = _bookingManager.TripItems.OfType<TripItem>().ToList();
                TripItem = tripItems.FirstOrDefault<TripItem>(x => x.BookingReference.Equals(BookingReference));
                TripItem.IsSummaryMode = false;
            }
            CardsVisibility = new FlightDetailCardsVisibility(HasCheckedIn, CanCheckIn);
        }

        public void Prepare(TripItem parameter)
        {
            TripItem = parameter;
            var selectedFlight = TripItem.FlightItems.FirstOrDefault(x => x.Number.Equals(TripItem.FlightNo));
            selectedFlight.BookingReference = parameter.BookingReference;
            Prepare(selectedFlight);
            CardsVisibility = new FlightDetailCardsVisibility(HasCheckedIn, CanCheckIn);
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            ProgressActivityService.Show();
            try
            {
                await RefreshFunc();
            }
            finally
            {
                ProgressActivityService.Hide();
            }
        }

        public override void ViewDisappearing()
        {
            DisposeObservers();
            base.ViewDisappearing();
        }

        public async Task<BookingItem> RefreshFuncLocal(bool prepareAfterLoad)
        {
            BookingEntity bookingEntity = await _bookingManager.LoadBookingAsync(BookingReference);
            BookingItem bookingItem = null;
            if (bookingEntity != null)
            {
                List<BookingItem> bookingItems = bookingEntity.PassengerSegments.Select(x => x.ToBookingItem(bookingEntity)).ToList();
                bookingItem = bookingItems.FirstOrDefault(x => x.Number.Equals(FullFlightNumber));

                CheckInItems = bookingEntity.ToCheckInItems(SegmentId, bookingItem).ToList();
                TravellerItems?.Clear();
                BookingItems?.Clear();
                foreach (CheckInItem checkInItem in CheckInItems)
                {
                    TravellerItems.AddRange(checkInItem.TravellerItems);
                    BookingItems.AddRange(checkInItem.BookingItems);
                }

                Prepare(bookingItem);
                await InitializeCheckIn();
                await InitializeWeatherItem();
            }
            return bookingItem;
        }

        public override async Task RefreshFunc()
        {
            BookingEntity bookingEntity = await _bookingManager.LoadBookingAsync(BookingReference);
            if (bookingEntity != null)
            {
                if (ConnectivityManager.NetworkAccess == Nacelle.KMA.Core.Enums.NetworkAccess.None)
                {
                    await ToastService.Show(Constants.Messages.NoInternetConnectdion, true);
                    return;
                }
                try
                {
                    //var response = await BookingManager.FindAndSaveBookingAsync(BookingReference, BookingLastName, false);
                    Response<BookingEntity> response = await BookingManager.QueryBookingAsync(BookingReference, BookingLastName);
                    if (response != null && response.IsSuccess)
                    {
                        BookingEntity updatedBookingEntity = response.Data;
                        List<BookingItem> updatedBookingItems = response.Data.PassengerSegments.Select(x => x.ToBookingItem(bookingEntity)).ToList();
                        BookingItem updatedBookingItem = updatedBookingItems.FirstOrDefault(x => x.Number.Equals(FullFlightNumber));

                        CheckInItems = updatedBookingEntity.ToCheckInItems(SegmentId, updatedBookingItem).ToList();
                        TravellerItems.Clear();
                        BookingItems.Clear();
                        foreach (CheckInItem checkInItem in CheckInItems)
                        {
                            TravellerItems.AddRange(checkInItem.TravellerItems);
                            BookingItems.AddRange(checkInItem.BookingItems);
                        }

                        Prepare(updatedBookingItem);
                        await InitializeCheckIn();
                        await InitializeWeatherItem();
                    }
                }
                catch (Exception ex)
                {
                    await Mvx.IoCProvider.Resolve<IAlertService>().Show("", ex.Message, ("Ok", null));
                }
            }
        }

        #endregion //View Model Callbacks

        #region Command Handlers

        private void DoTripItemMenuCommand(TripItem tripItem)
        {
            DoNavMenuCommand(tripItem.SelectedMenu);
        }

        private void DoNavMenuCommand(string menuText)
        {
            switch (menuText)
            {
                case Constants.ContextActions.ManageBooking:
                    LogEvent(Constants.Analytics.Target.ManageBooking, BookingReference);
                    NavigationService.Navigate<ManageBookingViewModel>();
                    break;
                case Constants.ContextActions.AddExtras:
                    LogEvent(Constants.Analytics.Target.AddExtras, BookingReference);
                    NavigationService.Navigate<FlightExtrasViewModel>();
                    break;
                case Constants.ContextActions.RemoveBooking:
                    LogEvent(Constants.Analytics.Target.RemoveFromApp, BookingReference);
                    MvxNotifyTask.Create(DoRemoveBookingAsync());
                    break;
            }

        }

        private async Task DoViewBoardingPassCommandAsyncTravellerItem(TravellerItem travellerItem)
        {
            DoViewBoardingPassCommandAsync(BookingItem);
        }

        private async Task DoViewBoardingPassCommandAsync(BookingItem bookingItem)
        {
            if (ConnectivityManager.NetworkAccess == Nacelle.KMA.Core.Enums.NetworkAccess.None)            {                await ToastService.Show(Constants.Messages.NoInternetConnectdion, true);                return;            }
            ProgressActivityService.Show();
            try
            {
                var bookingReference = bookingItem != null ? bookingItem.BookingReference : BookingReference;
                string segment = this.SegmentId;
                var response = await BookingManager.FindBookingAsync(bookingReference, BookingLastName);
                if (response != null)
                {
                    this.ConversationID = response.Data.ConversationID;
                    var checkinItems = response.Data.ToCheckInItems(SegmentId);
                    var lastName = bookingItem != null ? bookingItem.BookingLastName : BookingLastName;
                    await NavigationService.Navigate<BoardingPassViewModel, CheckedInNavBundle>(new CheckedInNavBundle
                    {
                        ConversationID = this.ConversationID,
                        BookingReference = bookingReference,
                        LastName = lastName,
                        CheckInItems = checkinItems.ToList()
                    });
                }
            }
            catch (Exception ex)            {                await Mvx.IoCProvider.Resolve<IAlertService>().Show("", ex.Message, ("Ok", null));            }            finally            {                ProgressActivityService.Hide();            }
        }

        private async Task DoCheckInCommandAsync(BookingItem bookingItem)
        {
            var bookingReference = bookingItem != null ? bookingItem.BookingReference : BookingReference;
            var response = await _bookingManager.LoadBookingAsync(bookingReference);
            if (response != null)
            {
                var checkinItems = response.ToCheckInItems();
                var lastName = bookingItem != null ? bookingItem.BookingLastName : BookingLastName;
                await NavigationService.Navigate<CheckInfoViewModel, CheckInNavBundle>(new CheckInNavBundle
                {
                    ConversationID = response.ConversationID,
                    BookingReference = bookingItem.BookingReference,
                    LastName = lastName,
                    CheckInItems = checkinItems.ToList()
                });
            }
        }

        private Task DoShareFlightCommandAsync()
        {
            return Share.RequestAsync(new ShareTextRequest
            {
                Text = $"Hi, my flight details are as follows\n\nFlight: {FullFlightNumber}\n{Caption}\nDeparts: {DateFrom.ToString("dd MMMM")} @ {DateFrom.ToString("HH:mm")}\nArrives: {DateTo.ToString("dd MMMM")} @ {DateTo.ToString("HH:mm")}",
                Title = "Share Flight"
            });
        }

        private async Task DoFlightItemAsync(string flightNumber)
        {
            var selectedFlight = TripItem.FlightItems.FirstOrDefault(x => x.Number.Equals(flightNumber));
            Prepare(selectedFlight);
            DisposeObservers();
            await InitializeCheckIn();
            await InitializeWeatherItem();
            ScrollToTop?.Invoke();
        }

        #endregion //Command Handlers

        #region Initialize Methods

        private async Task InitializeWeatherItem()
        {
            try
            {
                if (!CardsVisibility.IsWeatherCardVisible)
                {
                    return;
                }
                var departureHours = GetTimeToDeparture().TotalHours;
                var weatherEntity = await _weatherManager.GetOrFetchWeatherData(To);
                // Fetch latest current weather, not the first match in the future (5am)
                var arrivalWeather = weatherEntity.FirstOrDefault();
                if (arrivalWeather == null)
                {
                    Weather = null;
#if (DEBUG)
                    if (App.IsStubMode)
                    {
                        Weather = new WeatherItem
                        {
                            City = "Smallville",
                            Synopsis = "Sunny",
                            Icon = "0.png",
                            Temperature = "25"
                        };
                    }
#endif
                }
                else
                {
                    Weather = new WeatherItem
                    {
                        City = arrivalWeather.WeatherStation,
                        Synopsis = arrivalWeather.Description.ToTitleCase(),
                        Icon = arrivalWeather.Icon,
                        Temperature = Math.Ceiling(arrivalWeather.MaxTemperature).ToString()
                    };
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: Unable to get weather: " + ex.Message);
            }
        }

        private async Task InitializeCheckIn()
        {
            var isFutureBooking = GetTimeToDeparture().IsFutureBooking();
            var lessThan24Hours = GetTimeToDeparture().IsIn24HourWindow();
            var in48HourWindow = GetTimeToDeparture().IsIn48HourWindow();
            var departureWindow = GetTimeToDeparture();
            var isDayOfOperations = DateFrom.IsDayOfOperations();

             var statusResponse = await _bookingManager.GetFlightStatus(DateFrom, From, FlightNo);
            if (isDayOfOperations && statusResponse.IsStatusDelayed())
            {
                CardsVisibility = new FlightDetailCardsVisibility(TripType.Delayed, HasCheckedIn, CanCheckIn);
                return;
            }
            if (statusResponse.IsStatusCancelled())
            {
                CardsVisibility = new FlightDetailCardsVisibility(TripType.Cancelled, HasCheckedIn, CanCheckIn);
                return;
            }
            if (isFutureBooking && !in48HourWindow) //Flight is still in the future, not entered into the under 48 hour window.
            {
                CheckInMessage = "Online check-in opens";
                var checkInDate = DateFrom.AddDays(-1);
                CheckInDateTime = checkInDate.ToString("dd MMMM yyyy");
                FormattedCheckInTime = DateFrom.ToString("HH:mm").Replace(":", "h");
                CardsVisibility = new FlightDetailCardsVisibility(TripType.Future, HasCheckedIn, CanCheckIn);
            }
            else if (in48HourWindow) //Under 48 hours, but more than 24 hours i.e. check-in not available yet.
            {
                SetTimerDetails(GetTimeToCheckInOpening, "Online check-in opens in");
                FormattedCheckInTime = DateFrom.AddDays(-1).ToString("HH:mm").Replace(":", "h");
                CardsVisibility = new FlightDetailCardsVisibility(TripType.CheckInDayApproaching, HasCheckedIn, CanCheckIn);
            }
            else if (lessThan24Hours) //Under 24 hours, but less than 48 hours i.e. check-in is available.
            {
                SetTimerDetails(GetTimeToCheckInClosing, "Online check-in closes in");
                FormattedCheckInTime = DateFrom.AddHours(-1).ToString("HH:mm").Replace(":", "h");
                CardsVisibility = new FlightDetailCardsVisibility(TripType.CheckInDay, HasCheckedIn, CanCheckIn);
            }
            else if (departureWindow.TotalMinutes >= 10 && departureWindow.TotalMinutes < 60) //Check-in period has expired (less than 60 minutes to departure) and over 10 minutes to departure.
            {
                CardsVisibility = new FlightDetailCardsVisibility(TripType.Boarding, HasCheckedIn, CanCheckIn);
            }
            else if (departureWindow.TotalMinutes >= -30 && departureWindow.TotalMinutes < 10) //Ten minutes beore departure and 30 minutes after departure.
            {
                CardsVisibility = new FlightDetailCardsVisibility(TripType.LeavingSoon, HasCheckedIn, CanCheckIn);
            }
            else if (departureWindow.TotalMinutes < -30) //Flight has already left.
            {
                CardsVisibility = new FlightDetailCardsVisibility(TripType.Past, HasCheckedIn, CanCheckIn);
            }
            Debug.WriteLine("InitializeCheckIn: " + CardsVisibility.TripType.ToString());
        }

        private Dictionary<string, string> InitializeAnalyticsContextTravellerItem(TravellerItem bookingItem)
        {
            return InitializeAnalyticsContextBookingItem(BookingItem);
        }

        private Dictionary<string, string> InitializeAnalyticsContextBookingItem(BookingItem bookingItem)
        {
            return new AnalyticsContextBuilder().WithBookingReference(BookingReference)
                .WithFlightNo(FullFlightNumber)
                .WithOrigin(From)
                .WithDestination(To)
                .WithDepartureDate(DateFrom)
                .WithDepartureTime(DateFrom)
                .Build();
        }

        private Dictionary<string, string> InitializeBookingReferenceAnalyticsContext()
        {
            return new AnalyticsContextBuilder().WithBookingReference(FullFlightNumber).Build();
        }

        private async Task DoRemoveBookingAsync()
        {
            var alert = Mvx.IoCProvider.Resolve<IAlertService>();

            var yes = (Title: "Keep", Action: new Func<Task>(() => Task.FromResult(false)));
            var no = (Title: "Remove", Action: new Func<Task>(RemoveBookingAsync));

            await alert.Show("Remove trip from app?",
                "Your trip will be removed from this app.\nThis will not cancel your booking.",
                yes,
                no);
        }

        private async Task RemoveBookingAsync()
        {
            await _bookingManager.RemoveBooking(BookingReference);
            await NavigationService.Close(this);
        }

        private async Task DoFlightExtrasAsync() =>
           await NavigationService.Navigate<FlightExtrasViewModel>();

        private async Task DoManageBookingAsync() =>
            await NavigationService.Navigate<ManageBookingViewModel>();

        #endregion //Initialize Methods

        #region Utility Methods

        private string GetCheckinFormattedTimeRemaining(int hoursRemaining, int minutesRemaining)
        {
            var result = new StringBuilder();
            if (hoursRemaining > 0)
            {
                result.Append($"{hoursRemaining} hours ");
            }
            result.Append($"{minutesRemaining.ToString().PadLeft(2, '0')} mins");
            return result.ToString();
        }

        private TimeSpan GetTimeToCheckInClosing() => DateFrom.RoundDownMinute().AddHours(-1) - DateTime.Now.RoundDownMinute();

        private TimeSpan GetTimeToCheckInOpening() => DateFrom.RoundDownMinute().AddDays(-1) - DateTime.Now.RoundDownMinute();

        private TimeSpan GetTimeToBoarding() => BoardingTime.RoundDownMinute() - DateTime.Now.RoundDownMinute();

        private TimeSpan GetTimeToDeparture() => DateFrom.RoundDownMinute() - DateTime.Now.RoundDownMinute();

        #endregion //Utility Methods

        #region Logging Methods

        private void LogEvent(string target, string bookingReference)
        {
            Mvx.IoCProvider.Resolve<IAnalyticsService>().LogEvent(Constants.Analytics.Events.OptionsItemTap, target, new AnalyticsContextBuilder().WithBookingReference(bookingReference).Build());
        }

        #endregion //Logging Methods

        #region Timer Methods

        private void SetTimerDetails(Func<TimeSpan> timeSpanFunc, string message)
        {
            CheckInMessage = message;
            var timeDiff = timeSpanFunc.Invoke();
            CheckInDateTime = GetCheckinFormattedTimeRemaining(timeDiff.Hours, timeDiff.Minutes);
            _observers.Add(
                Observable.Interval(TimeSpan.FromSeconds(10))
                          .Subscribe(x =>
                          {
                              var updatedTimeDiff = timeSpanFunc.Invoke();
                              CheckInDateTime = GetCheckinFormattedTimeRemaining(updatedTimeDiff.Hours, updatedTimeDiff.Minutes);
                          }));
        }

        #endregion //Timer Methods

        #region Cleanup Methods

        private void DisposeObservers() => _observers.ForEach(x => x.Dispose());

        #endregion //Cleanup Methods

        #endregion //Methods
    }
}
