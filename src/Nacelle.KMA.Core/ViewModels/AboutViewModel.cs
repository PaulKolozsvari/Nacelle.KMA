#region Using Directives

using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using Nacelle.KMA.Core.Commands;
using Nacelle.KMA.Core.Data;
using Nacelle.KMA.Core.Models;
using Xamarin.Essentials;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public class AboutViewModel: BaseClosableViewModel
    {
        #region Constructors

        public AboutViewModel()
        {
            PrivacyPolicyCommand = new TrackableAsyncCommand(Constants.Analytics.Events.ButtonTap, DoPrivacyPolicyCommnadAsync, Constants.Analytics.Target.PrivacyPolicy);
            TermsConditionsCommand = new TrackableAsyncCommand(Constants.Analytics.Events.ButtonTap, DoTermsConditionsCommandAsync, Constants.Analytics.Target.TandC);

            //AppVersion = "1.5";
            AppVersion = $"{AppInfo.VersionString}.{AppInfo.BuildString}";
            Platform = DeviceInfo.Platform == DevicePlatform.iOS ? "iPhone" : "Android";
        }

        #endregion //Constructors

        #region Properties

        public string Platform { get; }
        public string AppVersion { get; }

        #endregion //Properties

        #region Commands

        public ICommand PrivacyPolicyCommand { get; }
        public ICommand TermsConditionsCommand { get; }

        #endregion //Commands

        #region Command Handlers

        private async Task DoTermsConditionsCommandAsync()
        {
            var content = DataFactory.CreateTermsConditionHtml();
            var webData = new WebDataModel("Terms & Conditions", content);
            await NavigationService.Navigate<WebPageViewModel, WebDataModel>(webData);
        }

        private async Task DoPrivacyPolicyCommnadAsync()
        {
            var content = DataFactory.CreatePrivacyPolicyHtml();
            var webData = new WebDataModel("Privacy Policy", content);
            await NavigationService.Navigate<WebPageViewModel, WebDataModel>(webData);
        }

        #endregion //Command Handlers
    }
}
