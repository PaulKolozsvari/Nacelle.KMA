#region Using Directives

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Nacelle.KMA.Core.Managers;
using Nacelle.KMA.Core.Models.Entites;
using Nacelle.KMA.Core.Models.Items;
using Nacelle.KMA.Core.NavBundles;
using Nacelle.KMA.Core.Platform;
using Nacelle.KMA.Core.Validators;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public class UpdateTravellerDetailsViewModel : BaseCheckInViewModel
    {
        #region Constructors

        public UpdateTravellerDetailsViewModel(ICheckInManager checkInManager, IViewModelValidator viewModelValidator, IProgressActivityService progressActivityService)
        {
            _checkInManager = checkInManager;
            _viewModelValidator = viewModelValidator;
            _progressActivityService = progressActivityService;
            _validator = new TravellerDetailsValidator();
            NextCommand = new MvxAsyncCommand(DoNextCommandAsync);
        }

        #endregion //Constructors

        #region Fields

        private readonly ICheckInManager _checkInManager;
        private readonly IProgressActivityService _progressActivityService;
        private readonly IViewModelValidator _viewModelValidator;
        private readonly TravellerDetailsValidator _validator;
        private Dictionary<string, string> _errors;

        #endregion //Fields

        #region Properties

        public MvxObservableCollection<TravellerItem> TravellerItems { get; private set; }

        public List<CountryEntity> CountryISOCodes { get; private set; }

        public List<CountryEntity> CountryDialCodes { get; private set; }

        public Dictionary<string, string> Errors
        {
            get => _errors;
            set => SetProperty(ref _errors, value);
        }

        public List<BookingItem> BookingItems { get; private set; }

        public bool IsNextEnabled => TravellerItems.Where(x => !x.IsInfant).All(x => x.SelectedWeightCategoryIndex != -1);

        #endregion //Properties

        #region Event Handlers

        public override void Prepare(CheckInNavBundle parameter)
        {
            base.Prepare(parameter);
            List<TravellerItem> travellerItems = new List<TravellerItem>();
            BookingItems = parameter.CheckInItems.FirstOrDefault()?.BookingItems.Where(x => x.IsKululaFlight).ToList();
            bool populateCountryDetails = false;
            foreach (CheckInItem checkInItem in parameter.CheckInItems)
            {
                if (checkInItem.TravellerItems.Any(x => x.RequiresEmergencyContact || x.RequiresPassport))
                {
                    populateCountryDetails = true;
                }
                if (checkInItem.TravellerItems.Any(x => x.DoCheckIn))
                {
                    travellerItems.Add(checkInItem.TravellerItems.FirstOrDefault());
                }
            }
            if (populateCountryDetails)
            {
                var countryManager = Mvx.IoCProvider.Resolve<ICountryManager>();
                CountryISOCodes = countryManager.GetCountryISOCodes();
                CountryDialCodes = countryManager.GetCountryDialCodes();
            }
            TravellerItems = new MvxObservableCollection<TravellerItem>(travellerItems);
        }

        public override async Task Initialize()
        {
            await base.Initialize();
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            foreach (var item in TravellerItems)
            {
                item.PropertyChanged += Handle_PropertyChanged;
            }
        }

        public override void ViewDisappearing()
        {
            base.ViewDisappearing();
            foreach (var item in TravellerItems)
            {
                item.PropertyChanged -= Handle_PropertyChanged;
            }
        }

        private void Handle_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TravellerItem.SelectedWeightCategoryIndex))
            {
                if (sender is TravellerItem travellerItem)
                {
                    travellerItem.ShouldUpdateWeightCategory = true;
                    RaisePropertyChanged(() => IsNextEnabled);
                }
            }
        }

        #endregion //Event Handlers

        #region Commands

        public MvxAsyncCommand NextCommand { get; }

        #endregion //Commands

        #region Command Handlers

        private async Task DoNextCommandAsync()
        {
            try
            {
                if (TravellerItems.Any(x => x.ShouldUpdateWeightCategory) ||
                    TravellerItems.Any(x => x.RequiresEmergencyContact) ||
                    TravellerItems.Any(x => x.RequiresPassport))
                {
                    var isValid = true;
                    Dictionary<string, string> errors = null;
                    foreach (var traveller in TravellerItems)
                    {
                        if (!_viewModelValidator.Validate(_validator, traveller, out errors))
                        {
                            isValid = false;
                            Errors = errors;
                            break;
                        }
                    }
                    if (!isValid)
                    {
                        return;
                    }
                    _progressActivityService.Show();
                    if (TravellerItems.Any(x => x.RequiresEmergencyContact) || TravellerItems.Any(x => x.RequiresPassport))
                    {
                        await _checkInManager.UpdatePassengersAsync(TravellerItems, Parameter);
                    }
                    else
                    {
                        var travellers = TravellerItems.Where(x => x.ShouldUpdateWeightCategory).ToDictionary(x => x.Id, x => x.SelectedWeightCategoryName);
                        await _checkInManager.UpdatePassengersAsync(travellers, Parameter);
                    }
                    await NavigationService.Navigate<CheckSeatsViewModel, CheckInNavBundle>(new CheckInNavBundle
                    {
                        ConversationID = Parameter.ConversationID,
                        BookingReference = Parameter.BookingReference,
                        LastName = Parameter.LastName,
                        CheckInItems = Parameter.CheckInItems.Where(x => x.TravellerItems.Any(y => y.DoCheckIn)).ToList()
                    });
                    foreach (var travellerItem in TravellerItems)
                    {
                        if (!travellerItem.IsInfant && travellerItem.ShouldUpdateWeightCategory)
                        {
                            travellerItem.ShouldUpdateWeightCategory = false;
                            travellerItem.HasUpdatedWeightCategory = true;
                        }
                    }
                    _progressActivityService.Hide();
                }
                else
                {
                    await NavigationService.Navigate<CheckSeatsViewModel, CheckInNavBundle>(new CheckInNavBundle
                    {
                        ConversationID = Parameter.ConversationID,
                        BookingReference = Parameter.BookingReference,
                        LastName = Parameter.LastName,
                        CheckInItems = Parameter.CheckInItems.Where(x => x.TravellerItems.Any(y => y.DoCheckIn)).ToList()
                    });
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

        #endregion //Command Handlers
    }
}
