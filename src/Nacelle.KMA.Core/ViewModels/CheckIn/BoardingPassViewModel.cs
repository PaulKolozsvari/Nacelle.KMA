#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Nacelle.KMA.Core.Commands;
using Nacelle.KMA.Core.Managers;
using Nacelle.KMA.Core.Mappers;
using Nacelle.KMA.Core.Models.Entites;
using Nacelle.KMA.Core.Models.Items;
using Nacelle.KMA.Core.NavBundles;
using Nacelle.KMA.Core.Platform;
using Xamarin.Essentials;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public class BoardingPassViewModel : BaseViewModel<CheckedInNavBundle>, ICanShare
    {
        #region Constructors

        public BoardingPassViewModel(ICheckInManager checkInManager)
        {
            _checkInManager = checkInManager;
            BoardingPassItems = new MvxObservableCollection<BoardingPassItem>();
            ShareBoardingPassCommand = new TrackableAsyncCommand<List<TravellerItem>>(Constants.Analytics.Events.BoardingPassShare, ShareBoardingpass);
            AddToWalletCommand = new MvxAsyncCommand<BoardingPassItem>(AddToWalletAsync);
        }

        #endregion //Constructors

        #region Fields

        private readonly ICheckInManager _checkInManager;

        #endregion //Fields

        #region Properties

        public List<TravellerItem> TravellerItems { get; private set; }

        #endregion //Properties

        #region Commands

        public MvxObservableCollection<BoardingPassItem> BoardingPassItems { get; }
        public TrackableAsyncCommand<List<TravellerItem>> ShareBoardingPassCommand { get; }
        public MvxAsyncCommand<BoardingPassItem> AddToWalletCommand { get; }

        #endregion //Commands

        #region Methods

        public override void Prepare(CheckedInNavBundle parameter)
        {
            base.Prepare(parameter);
            TravellerItems = new List<TravellerItem>();
            foreach (CheckInItem checkInItem in Parameter.CheckInItems)
            {
                TravellerItems.AddRange(checkInItem.TravellerItems);
            }
            if (Parameter.BoardingPassItems != null && Parameter.BoardingPassItems.Any())
            {
                BoardingPassItems.AddRange(Parameter.BoardingPassItems);
            }
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            if (!BoardingPassItems.Any())
            {
                var progressActivityService = Mvx.IoCProvider.Resolve<IProgressActivityService>();
                progressActivityService.Show();
                try
                {
                    List<string> test = Parameter.CheckInItems.SelectMany(x => x.BookingItems).Where(x => x.HasCheckedIn).Select(x => x.PassengerFlightId).Distinct().ToList();
                    List<string> passengerFlightIds = TravellerItems.Select(x => x.PassengerFlightId).Distinct().ToList();
                    List<BoardingPassEntity> boardingPassEntities = (await _checkInManager.GetBoardingPassesAsync(passengerFlightIds, Parameter)).ToList();
                    BoardingPassItems.AddRange(boardingPassEntities.Select(x => x.ToBoardingPassItem()));
                }
                catch (Exception ex)
                {
                    var alertService = Mvx.IoCProvider.Resolve<IAlertService>();
                    alertService.Show("", ex.Message, (Title: Constants.Text.OK, null));
                }
                finally
                {
                    progressActivityService.Hide();
                }
            }
        }

        private async Task ShareBoardingpass(List<TravellerItem> travellerItems)
        {
            try
            {
                var boardingPassPdfGenerator = Mvx.IoCProvider.Resolve<IBoardingPassPdfGenerator>();
                var bpi = BoardingPassItems.Where(x => travellerItems.Any(y => y.Name.Equals(x.PassengerName)));
                var filePath = boardingPassPdfGenerator.CreateBoardingpassPdf(bpi.ToList());

                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "kulula.com - Boarding pass",
                    File = new ShareFile(filePath)
                });
            }
            catch (Exception ex)
            {
                var alertService = Mvx.IoCProvider.Resolve<IAlertService>();
                alertService.Show("", ex.Message, (Title: Constants.Text.OK, null));
            }
        }

        private async Task AddToWalletAsync(BoardingPassItem boardingPassItem)
        {
            // Using IProgressActivityService causes an null refrence erro to be thrown on Hide();
            IsBusy = true;
            try
            {
                var bytes = await _checkInManager.FormattedForWalletBoardingPassesAsync(boardingPassItem.BoardingPassJson);
                var passKitService = Mvx.IoCProvider.Resolve<IPassKitService>();
                await passKitService.AddToWallet(bytes);
            }
            catch (Exception ex)
            {
                var alertService = Mvx.IoCProvider.Resolve<IAlertService>();
                alertService.Show("", ex.Message, (Title: Constants.Text.OK, null));
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion //Methods
    }
}
