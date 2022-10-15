#region Using Directives

using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using MvvmCross.Commands;
using Nacelle.KMA.Core.Models.Items;
using Nacelle.KMA.Core.NavBundles;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public class CheckTravellersViewModel : BaseCheckInViewModel
    {
        #region Constructors

        public CheckTravellersViewModel()
        {
            SelectTravellerCommand = new MvxCommand<TravellerItem>(t => t.DoCheckIn = !t.DoCheckIn);
            NextCommand = new MvxAsyncCommand(DoNextCommandAsync);
        }

        #endregion //Constructors

        #region Fields

        private bool _eventsSubscribed;

        #endregion //Fields

        #region Methods

        public override void Prepare(CheckInNavBundle parameter)
        {
            base.Prepare(parameter);
            TravellerItems = new List<TravellerItem>();
            BookingItems = parameter.CheckInItems.FirstOrDefault()?.BookingItems.Where(x => x.IsKululaFlight).ToList();

            foreach (CheckInItem checkInItem in parameter.CheckInItems)
            {
                if (checkInItem.TravellerItems.Any(x => !x.IsInfant))
                {
                    TravellerItems.Add(checkInItem.TravellerItems.FirstOrDefault());
                }
            }
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            SubscribeEvents();
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

        private void SubscribeEvents()
        {
            if (TravellerItems == null || _eventsSubscribed)
            {
                return;
            }
            foreach (var item in TravellerItems)
            {
                item.PropertyChanged += Handle_PropertyChanged;
            }
            _eventsSubscribed = true;
        }

        private void UnsubscribeEvents()
        {
            if (TravellerItems == null || !_eventsSubscribed)
            {
                return;
            }
            foreach (var item in TravellerItems)
            {
                item.PropertyChanged -= Handle_PropertyChanged;
            }
            _eventsSubscribed = false;
        }

        #endregion //Methods

        #region Commands

        public MvxCommand<TravellerItem> SelectTravellerCommand { get; }
        public MvxAsyncCommand NextCommand { get; }
        public List<TravellerItem> TravellerItems { get; private set; }
        public List<BookingItem> BookingItems { get; private set; }

        #endregion //Commands

        #region Command Handlers

        public bool IsNextEnabled => TravellerItems.Any(x => x.DoCheckIn);

        private void Handle_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TravellerItem.DoCheckIn))
            {
                RaisePropertyChanged(() => IsNextEnabled);
            }
        }

        private Task DoNextCommandAsync()
        {
            List<CheckInItem> selectedCheckInItems = Parameter.CheckInItems.Where(x => x.TravellerItems.Any(y => y.DoCheckIn)).ToList();
            if (selectedCheckInItems.Count < 1)
            {
                throw new System.Exception("No passengers selected.");
            }

            //List<TravellerItem> infantTravellerItems = checkInItem.TravellerItems.Where(x => x.DoCheckIn && x.HasInfant).ToList();
            //List<string> infantTravellerIds = new List<string>();
            //foreach (TravellerItem travellerItem in checkInItem.TravellerItems)
            //{
            //    if (!infantTravellerIds.Contains(travellerItem.InfantId))
            //    {
            //        infantTravellerIds.Add(travellerItem.InfantId);
            //    }
            //}
            //if (infantTravellerIds.Count > 0)
            //{

            //    var infantTravellers = Parameter.CheckInItems.Where(x => x.TravellerItems.Any(y => y.IsInfant) && infantTravellerIds.Contains(x.TravellerItems.Id)).ToList();
            //    checkinItems = checkinItems.Union(infantTravellers);
            //}
            return NavigationService.Navigate<UpdateTravellerDetailsViewModel, CheckInNavBundle>(new CheckInNavBundle
            {
                ConversationID = Parameter.ConversationID,
                BookingReference = Parameter.BookingReference,
                LastName = Parameter.LastName,
                CheckInItems = selectedCheckInItems
            });
        }

        #endregion //Command Handlers
    }
}
