#region Using Directives

using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using Nacelle.KMA.API.Models.Responses;
using Nacelle.KMA.Core.Managers;
using Nacelle.KMA.Core.Mappers;
using Nacelle.KMA.Core.Messages;
using Nacelle.KMA.Core.Models.Items;
using Nacelle.KMA.Core.NavBundles;
using Nacelle.KMA.Core.Platform;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public class CheckSeatsViewModel : BaseCheckInViewModel
    {
        #region Constructors

        public CheckSeatsViewModel(
            IBookingManager bookingManager,
            ICheckInManager checkInManager,
            IProgressActivityService progressActivityService,
            IMvxMessenger mvxMessenger)
        {
            _bookingManager = bookingManager;
            _checkInManager = checkInManager;
            _progressActivityService = progressActivityService;
            _mvxMessenger = mvxMessenger;

            _messageToken = _mvxMessenger.SubscribeOnMainThread<SeatsSelectedMessage>(SeatsSelectedMessageHandler);

            CheckInCommand = new MvxAsyncCommand(CheckInAsync);
        }

        #endregion //Constructors

        #region Fields

        private readonly IBookingManager _bookingManager;
        private readonly ICheckInManager _checkInManager;
        private readonly IProgressActivityService _progressActivityService;
        private readonly IMvxMessenger _mvxMessenger;
        private readonly MvxSubscriptionToken _messageToken;
        private bool _seatUpdated;
        private CheckSeatBookingItem _checkSeatBookingItem;

        #endregion //Fields

        #region Properties

        public List<BookingItem> BookingItems { get; private set; }

        // Nect button is enabled by default
        public bool IsNextEnabled => true;  // TravellerItems.All(x => x.Seat != null);

        public bool SeatUpdated { get => _seatUpdated; private set => SetProperty(ref _seatUpdated, value); }

        public List<CheckSeatBookingItem> CheckSeatBookingItems { get; private set; }

        public MvxObservableCollection<TravellerItem> TravellerItems { get; private set; }

        #endregion //Properties

        #region Methods

        public override void Prepare(CheckInNavBundle parameter)
        {
            base.Prepare(parameter);
            CheckSeatBookingItems = new List<CheckSeatBookingItem>();
            //BookingItems = Parameter.CheckInItems.SelectMany(x => x.BookingItems).ToList(); //Old code
            BookingItems = parameter.CheckInItems.FirstOrDefault()?.BookingItems.Where(x => x.IsKululaFlight).ToList();
            foreach (BookingItem bookingItem in BookingItems) //One Booking Item per sector.
            {
                CheckSeatBookingItem checkSeatBookingItem = new CheckSeatBookingItem() //One CheckSeatBookingItem per sector
                {
                    From = bookingItem.From,
                    To = bookingItem.To,
                    SegmentId = bookingItem.SegmentId,
                };
                checkSeatBookingItem.TravellerItems = new List<TravellerSelectSeatItem>(); //One per passenger for the sector i.e. BookingItem.
                List<TravellerItem> travellerItems = GetTravellerItemsForBookingItem(bookingItem, parameter.CheckInItems);
                foreach (TravellerItem travellerItem in travellerItems)
                {
                    TravellerSelectSeatItem travellerSelectSeatItem = new TravellerSelectSeatItem(travellerItem, travellerItem.PassengerFlightId, new SeatItem(travellerItem.Seat));
                    checkSeatBookingItem.TravellerItems.Add(travellerSelectSeatItem);
                }
                CheckSeatBookingItems.Add(checkSeatBookingItem);
            }
        }

        public List<TravellerItem> GetTravellerItemsForBookingItem(BookingItem bookingItem, List<CheckInItem> checkInItems)
        {
            List<TravellerItem> result = new List<TravellerItem>();
            foreach (CheckInItem checkInItem in checkInItems) //Gp through list of passengers i.e. checkin items.
            {
                List<TravellerItem> bookingTravellerItems = checkInItem.TravellerItems.Where(x => !x.IsInfant && x.SegmentId.ToLower().Equals(bookingItem.SegmentId)).ToList();
                result.AddRange(bookingTravellerItems);
            }
            return result;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            foreach (var item in CheckSeatBookingItems)
            {
                item.TravellerSelected += TravellerSelected;
            }
        }

        protected override Task PopToRoot()
        {
            _mvxMessenger.Unsubscribe<SeatsSelectedMessage>(_messageToken);
            return base.PopToRoot();
        }

        private Task SelectSeatAsync(CheckSeatBookingItem checkSeatBookingItem, TravellerSelectSeatItem traveller)
        {
            _checkSeatBookingItem = checkSeatBookingItem;

            return NavigationService.Navigate<SelectSeatViewModel, SelectSeatCheckInNavBundle>(new SelectSeatCheckInNavBundle(Parameter)
            {
                SelectedTraveller = traveller,
                SelectedSegmentId = checkSeatBookingItem.SegmentId,
                TravellerItems = checkSeatBookingItem.TravellerItems
            });
        }

        private void SeatsSelectedMessageHandler(SeatsSelectedMessage message)
        {
            MvxNotifyTask.Create(() => SubmitSeatRequestAsync(message.FlightIdSeatPairs));
        }

        private async Task SubmitSeatRequestAsync(IDictionary<string, string> selectedSeats)
        {
            if (selectedSeats != null && selectedSeats.Any())
            {
                _checkSeatBookingItem.SeatUpdated = true;

                _progressActivityService.Show();

                try
                {
                    foreach (var selectedSeat in selectedSeats)
                    {
                        if (!string.IsNullOrEmpty(selectedSeat.Value))
                        {
                            await _checkInManager.SelectSeatsAsync(selectedSeat.Value, selectedSeat.Key, Parameter);
                        }
                    }
                }
                catch (Exception ex)
                {
                    var alertService = Mvx.IoCProvider.Resolve<IAlertService>();
                    await alertService.Show("", ex.Message, (Title: Constants.Text.OK, null));
                }
                finally
                {
                    _progressActivityService.Hide();
                }
            }
        }

        private async Task CheckInAsync()
        {
            _progressActivityService.Show();

            try
            {
                var passengerFlightIds = BookingItems.Select(x => x.PassengerFlightId).ToList();

                var passengerIds = new List<string>();
                foreach (var travellerItem in CheckSeatBookingItems.FirstOrDefault()?.TravellerItems)
                {
                    passengerIds.Add(travellerItem.Id);
                    if (travellerItem.HasInfant)
                    {
                        passengerIds.Add(travellerItem.InfantPassengerId);
                    }
                }

                var boardingPassEntities = await _checkInManager.CheckInAsync(passengerIds, passengerFlightIds, Parameter);

                var bpItems = boardingPassEntities.Select(x => x.ToBoardingPassItem());

                await _bookingManager.FindAndSaveBookingAsync(Parameter.BookingReference, Parameter.LastName);

                _progressActivityService.Hide();

                var checkedInNavBundle = new CheckedInNavBundle(Parameter)
                {
                    BoardingPassItems = bpItems.ToList()
                };

                await NavigationService.Navigate<CheckInSuccessViewModel, CheckedInNavBundle>(checkedInNavBundle);
            }
            catch (Exception ex)
            {
                var alertService = Mvx.IoCProvider.Resolve<IAlertService>();
                await alertService.Show("", ex.Message, (Title: Constants.Text.OK, null));
            }
            finally
            {
                _mvxMessenger.Unsubscribe<SeatsSelectedMessage>(_messageToken);
                _progressActivityService.Hide();
            }
        }

        private async void TravellerSelected(object sender, TravellerSelectSeatItem traveller)
        {
            if (sender is CheckSeatBookingItem checkSeatBookingItem)
            {
                await SelectSeatAsync(checkSeatBookingItem, traveller);
            }
        }

        #endregion //Methods

        #region Commands

        public MvxAsyncCommand CheckInCommand { get; }

        #endregion //Commands
    }
}
