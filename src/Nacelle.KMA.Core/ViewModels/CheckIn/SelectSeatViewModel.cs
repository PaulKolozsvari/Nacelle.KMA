#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using Nacelle.KMA.Core.Data;
using Nacelle.KMA.Core.Managers;
using Nacelle.KMA.Core.Mappers;
using Nacelle.KMA.Core.Messages;
using Nacelle.KMA.Core.Models;
using Nacelle.KMA.Core.Models.Enums;
using Nacelle.KMA.Core.Models.Items;
using Nacelle.KMA.Core.NavBundles;
using Nacelle.KMA.Core.Platform;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public class SelectSeatViewModel : BaseViewModel<SelectSeatCheckInNavBundle>
    {
        #region Constructors

        public SelectSeatViewModel(
            ICheckInManager checkinManager,
            IProgressActivityService progressActivityService,
            IMvxMessenger mvxMessenger)
        {
            _checkinManager = checkinManager;
            _progressActivityService = progressActivityService;
            _mvxMessenger = mvxMessenger;
            CancelCommand = new MvxAsyncCommand(DoCancelAsync);
            ConfirmSelectionCommand = new MvxAsyncCommand(DoConfirmSelectionAsync);
            OpenHelpCommand = new MvxCommand(() => IsHelpVisible = true);
            CloseHelpCommand = new MvxCommand(() => IsHelpVisible = false);
            SelectTravellerCommand = new MvxCommand<TravellerSelectSeatItem>(DoSelectTraveller);
        }

        #endregion //Constructors

        #region Fields

        private readonly ICheckInManager _checkinManager;
        private readonly IProgressActivityService _progressActivityService;
        private readonly IMvxMessenger _mvxMessenger;

        private bool _eventsSubscribed;
        private bool _isHelpVisible;
        private IEnumerable<SeatItem> _seatItems;
        private MvxObservableCollection<RowItem> _rowItems;
        private TravellerSelectSeatItem _selectedTraveller;

        private Dictionary<string, string> _selectedSeats;
        private IReadOnlyDictionary<string, SeatItem> _originalSeats;

        #endregion //Fields

        #region Properties

        public MvxObservableCollection<RowItem> RowItems { get => _rowItems; private set => SetProperty(ref _rowItems, value); }
        public TravellerSelectSeatItem SelectedTraveller { get => _selectedTraveller; private set => SetProperty(ref _selectedTraveller, value); }
        public List<TravellerSelectSeatItem> AllTravellers => Parameter.TravellerItems;

        #endregion //Properties

        #region Commands

        public MvxAsyncCommand CancelCommand { get; }
        public MvxAsyncCommand ConfirmSelectionCommand { get; }
        public MvxCommand OpenHelpCommand { get; }
        public MvxCommand CloseHelpCommand { get; }
        public MvxCommand<TravellerSelectSeatItem> SelectTravellerCommand { get; }

        #endregion //Commands

        #region Methods

        public bool IsHelpVisible { get => _isHelpVisible; set => SetProperty(ref _isHelpVisible, value); }

        public override void Prepare(SelectSeatCheckInNavBundle parameter)
        {
            base.Prepare(parameter);
            SelectedTraveller = Parameter.SelectedTraveller;
            if (SelectedTraveller.SeatItem != null)
            {
                SelectedTraveller.SeatItem.SeatStatus = SeatStatus.Selected;
            }
            AllTravellers
                .Where(x => x.Id != Parameter.SelectedTraveller.Id && x.SeatItem != null)
                .ForEach(t => t.SeatItem.SeatStatus = SeatStatus.Traveller);
            // used for resetting when cancelling
            _originalSeats = new ReadOnlyDictionary<string, SeatItem>(AllTravellers.ToDictionary(x => x.Id, x => x.SeatItem));
            _selectedSeats = AllTravellers.Where(x => x.Id != Parameter.SelectedTraveller.Id && x.SeatItem != null).ToDictionary(x => x.PassengerFlightId, x => x.SeatItem.Seat);
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            try
            {
                _progressActivityService.Show();
                var segmentIds = Parameter.SelectedSegmentId;
                var seatMapEntity = await _checkinManager.GetSeatMapAsync(Parameter.SelectedSegmentId, Parameter);
                var hasInfantOrIsChild = AllTravellers.Any(x => x.HasInfantOrIsChild);
                var rowItems = seatMapEntity.ToRowItems(hasInfantOrIsChild).Where(x => x.Row > 0).ToList();
                RowItems = new MvxObservableCollection<RowItem>(rowItems);
                _seatItems = RowItems.SelectMany(x => x.SeatItems);
                SubscribeEvents();
                //pre-select seats if already have seats selected
                foreach (var traveller in AllTravellers)
                {
                    SeatItem seat = null;

                    if (traveller.SeatItem != null)
                    {
                        seat = _seatItems.SingleOrDefault(x => x.Seat.Equals(traveller.Seat, StringComparison.OrdinalIgnoreCase));

                        if (seat != null)
                        {
                            seat.SeatStatus = traveller.SeatItem.SeatStatus;

                            seat.Label = AllTravellers.Count > 1 ? traveller.Index.ToString() : string.Empty;

                            traveller.SeatItem = seat;
                        }
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

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            SubscribeEvents();
        }

        public override void ViewDisappearing()
        {
            base.ViewDisappearing();
            UnsubscribeEvents();
        }

        private async Task DoConfirmSelectionAsync()
        {
            if (Parameter.TravellerItems.Any(x => x.SeatItem != null && x.SeatItem.IsExit))
            {
                var acceptedTerms = await NavigateToExitTermsAsync();

                if (!acceptedTerms)
                {
                    return;
                }
            }
            await NavigationService.Close(this);
            _mvxMessenger.Publish(new SeatsSelectedMessage(this) { FlightIdSeatPairs = _selectedSeats });
        }

        private Task<bool> NavigateToExitTermsAsync()
        {
            var content = DataFactory.CreateExitRowTermsHtml();
            var webData = new WebDataModel("Exit Row Seat", content);
            return NavigationService.Navigate<ExitRowTermsViewModel, WebDataModel, bool>(webData);
        }

        private Task DoCancelAsync()
        {
            // unselect all seats selected in this session
            foreach (var traveller in AllTravellers)
            {
                if (_originalSeats.TryGetValue(traveller.Id, out var seatItem))
                {
                    traveller.SeatItem = seatItem;
                }
            }
            return NavigationService.Close(this);
        }

        private void DoSelectTraveller(TravellerSelectSeatItem traveller)
        {
            if (SelectedTraveller.Id == traveller.Id)
            {
                return;
            }
            // update previously-selected traveller's seat status
            if (SelectedTraveller.SeatItem != null)
            {
                SelectedTraveller.SeatItem.SeatStatus = SeatStatus.Traveller;
            }
            SelectedTraveller = traveller;
            if (traveller.SeatItem != null)
            {
                traveller.SeatItem.SeatStatus = SeatStatus.Selected;
                traveller.SeatItem.Label = AllTravellers.Count > 1 ? traveller.Index.ToString() : string.Empty;
            }
        }

        private void SubscribeEvents()
        {
            if (_seatItems == null || _eventsSubscribed)
            {
                return;
            }
            foreach (var seat in _seatItems)
            {
                seat.PropertyChanging += Seat_PropertyChanging;
                seat.PropertyChanged += Seat_PropertyChanged;
            }
            _eventsSubscribed = true;
        }

        private void UnsubscribeEvents()
        {
            if (_seatItems == null || !_eventsSubscribed)
            {
                return;
            }
            foreach (var seat in _seatItems)
            {
                seat.PropertyChanging -= Seat_PropertyChanging;
                seat.PropertyChanged -= Seat_PropertyChanged;
            }
            _eventsSubscribed = false;
        }

        private void Seat_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SeatItem.SeatStatus))
            {
                if (sender is SeatItem seatItem && seatItem.SeatStatus == SeatStatus.Selected)
                {
                    if (_selectedSeats.ContainsKey(SelectedTraveller.PassengerFlightId))
                    {
                        _selectedSeats[SelectedTraveller.PassengerFlightId] = seatItem.Seat;
                    }
                    else
                    {
                        _selectedSeats.Add(SelectedTraveller.PassengerFlightId, seatItem.Seat);
                    }
                    SelectedTraveller.SeatItem = seatItem;
                    SelectedTraveller.SeatItem.Label = AllTravellers.Count > 1 ? SelectedTraveller.Index.ToString() : string.Empty;
                }
            }
        }

        private void Seat_PropertyChanging(object sender, System.ComponentModel.PropertyChangingEventArgs e)
        {
            if (e.PropertyName == nameof(SeatItem.SeatStatus))
            {
                var currentSelected = _seatItems.FirstOrDefault(x => x.SeatStatus == SeatStatus.Selected);
                if (currentSelected != null && currentSelected != sender && _selectedSeats != null)
                {
                    if (sender is SeatItem changingSeatItem && e is MvxPropertyChangingEventArgs<SeatStatus> newStatus)
                    {
                        // Avalable => Selected
                        if (changingSeatItem.SeatStatus == SeatStatus.Available && newStatus.NewValue == SeatStatus.Selected)
                        {
                            if (_selectedSeats.ContainsKey(SelectedTraveller.PassengerFlightId))
                            {
                                _selectedSeats.Remove(SelectedTraveller.PassengerFlightId);
                            }

                            currentSelected.SeatStatus = SeatStatus.Available;
                            currentSelected.Label = string.Empty;
                        }
                    }
                }
            }
        }

        #endregion //Methods
    }
}
