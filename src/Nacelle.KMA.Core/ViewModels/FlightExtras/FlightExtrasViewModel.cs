#region Using Directives

using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross;
using MvvmCross.Commands;
using Nacelle.KMA.Core.Analytics;
using Nacelle.KMA.Core.Commands;
using Nacelle.KMA.Core.Models.Items;
using Xamarin.Essentials;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public class FlightExtrasViewModel: BaseClosableViewModel
    {
        #region Constructors

        public FlightExtrasViewModel()
        {
            PurchaseExtrasCommand = new TrackableAsyncCommand(Constants.Analytics.Events.ButtonTap, DoPurchaseExtrasCommandAsync, Constants.Analytics.Target.PurchaseOnline);
            CallCommand = new MvxCommand(DoCallCommand);
            FlightExtraItems = new List<FlightExtraItem>();
            InitializeFlightExtraItems();
        }

        #endregion //Constructors

        #region Properties

        public List<FlightExtraItem> FlightExtraItems { get; }

        #endregion //Properties

        #region Methods

        private void InitializeFlightExtraItems()
        {
            FlightExtraItems.Add(new FlightExtraItem("Extra Bags", "Save by purchasing extra bags onine up to 2 hours before flying", "iron-icon-extra-bags.svg"));
            FlightExtraItems.Add(new FlightExtraItem("Pre-paid Seats", "Secure your favorite spot for a small fee", "iron-icon-prepaid-seats.svg"));
            FlightExtraItems.Add(new FlightExtraItem("Flight & bag cover", "Take cover for yourself, your baggage and valuables against loss, damage, delay or theft, including missed flight and event protection", "iron-icon-flight-and-bag-cover.svg"));
            FlightExtraItems.Add(new FlightExtraItem("Q-Jump", "Skip long flight boarding queues at the airport and be among hte first to board acraift, access hand-luggage stowage, have you checked baggage offloaded at destination airport", "iron-icon-q-jump.svg"));
            FlightExtraItems.Add(new FlightExtraItem("SLOW XS Lounge", "Unwind and enjoy the modern SLOW XS Lounge at the Lanseria International airport", "iron-icon-slow-xs-lounge.svg"));
            FlightExtraItems.Add(new FlightExtraItem("Special Request", "Call us to arrange special assistance onboard our flights", "iron-icon-special-request.svg"));
            FlightExtraItems.Add(new FlightExtraItem("Dietary Requirements", "Call us to arrange special meals for you", "iron-icon-dietary-requirements.svg"));
        }

        #endregion //Methods

        #region Commands

        public ICommand PurchaseExtrasCommand { get; }
        public MvxCommand CallCommand { get; }

        #endregion //Commands

        #region Command Handlers

        private void DoCallCommand()
        {
            try
            {
                Mvx.IoCProvider.Resolve<IAnalyticsService>().LogEvent(Constants.Analytics.Events.ButtonTap, Constants.Analytics.Target.Phone);
                PhoneDialer.Open(Constants.KululaPhoneNo);
            }
            catch
            {
                Debug.WriteLine("No phone on this device");
            }
        }

        private Task DoPurchaseExtrasCommandAsync()
        {
            return Browser.OpenAsync(Constants.KululaManageBookingURL, BrowserLaunchMode.SystemPreferred);
        }

        #endregion //Command Handlers
    }

}
