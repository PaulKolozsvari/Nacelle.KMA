#region Using Directives

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Nacelle.KMA.Core.ViewModels;
using System.Diagnostics;
using Nacelle.KMA.Core.Commands;
using Nacelle.KMA.Core.Builders;
using System.Linq;

#endregion //Using Directives

namespace Nacelle.KMA.Core.Models.Items
{
    public class  TripItem : MvxNotifyPropertyChanged, ITripItem
    {
        #region Constructors

        public TripItem()
        {
            IsSummaryMode = true;
            FlightItems = new List<BookingItem>();
        }

        #endregion //Constructors

        #region Fields

        private bool _isSummaryMode;

        #endregion //Fields

        #region Properties

        public string Day { get; set; }

        public string Month { get; set; }

        public string Caption { get; set; }

        public string BookingReference { get; set; }

        public string ConversationID { get; set; }

        public string FlightNo { get; set; }

        public DateTime FromTime { get; set; }

        public Action<TripItem> SummaryModeToggled;

        public DateTime TripEndDate => FlightItems.Max(x => x.DateTo);

        public bool IsSummaryMode
        {
            get => _isSummaryMode;
            set
            {
                if (SetProperty(ref _isSummaryMode, value))
                {
                    SummaryModeToggled?.Invoke(this);
                }
            }
        }

        public List<BookingItem> FlightItems { get; set; }

        // TODO: Need a better mechanism to get the selected menu back to the parent
        public string SelectedMenu { get; set; }

        #endregion //Properties

        #region Commands

        public ICommand FlightItemCommand;

        public TrackableAsyncCommand<string> FlightDetailCommand => new TrackableAsyncCommand<string>(Constants.Analytics.Events.CardTap, DoFlightDetailCommandAsync, Constants.Analytics.Target.FlightCard, InitializeAnalyticsContext);

        public MvxCommand ToggleSummaryModeCommand => new MvxCommand(() => IsSummaryMode = !IsSummaryMode);

        #endregion //Commands

        #region Command Handlers

        private async Task DoFlightDetailCommandAsync(string flightNumber)
        {
            var navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
            this.FlightNo = flightNumber;
            await navigationService.Navigate<FlightDetailViewModel, TripItem>(this);
        }

        private Dictionary<string, string> InitializeAnalyticsContext(string flightNo)
        {
            var selectedFlight = FlightItems.FirstOrDefault(x => x.Number.Equals(flightNo));

            return new AnalyticsContextBuilder().WithBookingReference(FlightNo)
                .WithFlightNo(selectedFlight.Number)
                .WithOrigin(selectedFlight.From)
                .WithDestination(selectedFlight.To)
                .WithDepartureDate(selectedFlight.DateFrom)
                .WithDepartureTime(selectedFlight.DateFrom)
                .Build();
        }

        #endregion //Command Handlers
    }
}
