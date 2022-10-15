#region Using Directives

using System;
using System.IO;
using System.Threading.Tasks;
using MvvmCross.Commands;
using Nacelle.KMA.Core.Data;
using Nacelle.KMA.Core.NavBundles;
using Nacelle.KMA.Core.Platform;
using Xamarin.Essentials;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public class CheckInfoViewModel : BaseViewModel<CheckInNavBundle>
    {
        #region Constructors

        public CheckInfoViewModel(IPlatformPath platformPath)
        {
            HtmlContent = string.Empty;
            BaseUrl = platformPath.Get();

            CancelCommand = new MvxAsyncCommand(DoCancelCommandAsync);
            TermsCommand = new MvxAsyncCommand(DoTermsCommandAsync);
            NextCommand = new MvxAsyncCommand(DoNextCommandAsync);
        }

        #endregion //Constructors

        #region Fields

        private bool _isTermsScrolled;
        private bool _isTermsAccepted;

        #endregion //Fields

        #region Methods

        public string BaseUrl { get; }

        public string HtmlContent { get; private set; }

        public bool IsNextEnabled => IsTermsScrolled && IsTermsAccepted;

        public bool IsTermsScrolled
        {
            get => _isTermsScrolled;
            set
            {
                if (SetProperty(ref _isTermsScrolled, value))
                {
                    RaisePropertyChanged(() => IsNextEnabled);
                }
            }
        }

        public bool IsTermsAccepted
        {
            get => _isTermsAccepted;
            set
            {
                if (SetProperty(ref _isTermsAccepted, value))
                {
                    RaisePropertyChanged(() => IsNextEnabled);
                }
            }
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            var content = DataFactory.CreateCheckInfoHtml();
            HtmlContent = string.Format(Constants.HtmlFormatString, content);
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            IsTermsAccepted = false;
        }

        #endregion //Methods

        #region Commands

        public MvxAsyncCommand CancelCommand { get; }
        public MvxAsyncCommand TermsCommand { get; }
        public MvxAsyncCommand NextCommand { get; }

        #endregion //Commands

        #region Command Handlers

        private async Task DoNextCommandAsync()
        {
            await NavigationService.Navigate<CheckTravellersViewModel, CheckInNavBundle>(Parameter);
        }

        private async Task DoTermsCommandAsync()
        {
            await Browser.OpenAsync(Constants.KululaTermsUrl, BrowserLaunchMode.External);
        }

        private async Task DoCancelCommandAsync()
        {
            await NavigationService.Close(this);
        }

        #endregion //Command Handlers
    }
}
