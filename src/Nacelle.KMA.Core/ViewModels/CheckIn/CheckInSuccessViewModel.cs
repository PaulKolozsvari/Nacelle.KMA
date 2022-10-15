#region Using Directives

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Plugin.Messenger;
using MvvmCross.Presenters.Hints;
using Nacelle.KMA.Core.Commands;
using Nacelle.KMA.Core.Models.Items;
using Nacelle.KMA.Core.Models.Messages;
using Nacelle.KMA.Core.NavBundles;
using Nacelle.KMA.Core.Platform;
using Xamarin.Essentials;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public class CheckInSuccessViewModel : BaseViewModel<CheckedInNavBundle>, ICanShare
    {
        #region Constructors

        public CheckInSuccessViewModel(IMvxMessenger mvxMessenger)
        {
            _mvxMessenger = mvxMessenger;
            HomeCommand = new MvxAsyncCommand(NavigateHomeAsync);
            ViewBoardingPassCommand = new MvxAsyncCommand(ViewBoardingPassAsync);
            ShareBoardingPassCommand = new TrackableAsyncCommand<List<TravellerItem>>(Constants.Analytics.Events.BoardingPassShare, ShareBoardingPassAsync);
            CheckInNextFlightCommand = new MvxAsyncCommand(CheckInNextFlightAsync);
        }

        #endregion //Constructors

        #region Fields

        private readonly IMvxMessenger _mvxMessenger;

        #endregion //Fields

        #region Properties

        public List<TravellerItem> TravellerItems { get; private set; }

        #endregion //Properties

        #region Methods

        public override void Prepare(CheckedInNavBundle parameter)        {            base.Prepare(parameter);            TravellerItems = new List<TravellerItem>();            foreach (CheckInItem checkInItem in Parameter.CheckInItems)            {                foreach (TravellerItem travellerItem in checkInItem.TravellerItems)                {                    if (!TravellerItems.Any(p => p.Name == travellerItem.Name))                    {                        TravellerItems.Add(travellerItem);                    }                }                //TravellerItems.AddRange(checkInItem.TravellerItems);            }        }

        private async Task CheckInNextFlightAsync()
        {
            await NavigationService.ChangePresentation(new MvxPopToRootPresentationHint());
            _mvxMessenger.Publish(new ShowViewModelMessage(this, typeof(CheckInViewModel)));
        }

        private async Task ShareBoardingPassAsync(List<TravellerItem> travellerItems)
        {
            try
            {
                var boardingPassPdfGenerator = Mvx.IoCProvider.Resolve<IBoardingPassPdfGenerator>();
                List<BoardingPassItem> boardingPassItems = Parameter.BoardingPassItems.Where(x => travellerItems.Any(y => y.Name.Equals(x.PassengerName))).ToList();
                var filePath = boardingPassPdfGenerator.CreateBoardingpassPdf(boardingPassItems.ToList());
                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "kulula.com - Boarding pass",
                    File = new ShareFile(filePath)
                });
            }
            catch
            {
                // just suppress for now
            }

            // TODO :: when to delete?
            //try
            //{
            //    System.IO.File.Delete(filePath);
            //}
            //catch
            //{
            //    // just suppress
            //}
        }

        private async Task ViewBoardingPassAsync()
        {
            await NavigationService.Navigate<BoardingPassViewModel, CheckedInNavBundle>(Parameter);
        }

        private async Task NavigateHomeAsync()
        {
            await NavigationService.ChangePresentation(new MvxPopToRootPresentationHint());
            _mvxMessenger.Publish(new ShowViewModelMessage(this, typeof(TripsViewModel)));
        }

        #endregion //Methods

        #region Commands

        public MvxAsyncCommand HomeCommand { get; }
        public MvxAsyncCommand ViewBoardingPassCommand { get; }
        public TrackableAsyncCommand<List<TravellerItem>> ShareBoardingPassCommand { get; }
        public MvxAsyncCommand CheckInNextFlightCommand { get; }

        #endregion //Commands
    }
}
