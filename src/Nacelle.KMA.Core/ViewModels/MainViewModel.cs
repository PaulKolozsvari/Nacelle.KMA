#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Plugin.Messenger;
using Nacelle.KMA.Core.Analytics;
using Nacelle.KMA.Core.Managers;
using Nacelle.KMA.Core.Models.Messages;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(IMvxMessenger mvxMessenger, IDataMigrationManager dataMigrationManager)
        {
            NavigateTabViewModelsCommand = new MvxAsyncCommand(DoNavigateTabViewModelsCommandAsync);
            _subscribeToken = mvxMessenger.Subscribe<ShowViewModelMessage>(HandleShowViewModelMessage);

            _viewModelTypes = new[] { typeof(HomeViewModel), typeof(TripsViewModel), typeof(CheckInViewModel), typeof(MenuViewModel) };
            _dataMigrationManager = dataMigrationManager;
        }

        #region Fields

        private readonly MvxSubscriptionToken _subscribeToken;
        private readonly IList<Type> _viewModelTypes;
        private readonly IDataMigrationManager _dataMigrationManager;
        private int _currentTabIndex;

        #endregion //Fields

        #region Properties

        public int CurrentTabIndex
        {
            get => _currentTabIndex;
            set
            {
                _currentTabIndex = value;
                RaisePropertyChanged(() => CurrentTabIndex);
            }
        }

        #endregion //Properties

        #region Methods

        public override void ViewAppeared()
        {
            base.ViewAppeared();
        }

        public void TabSelected(string pageName)
        {
            switch (pageName.ToLowerInvariant())
            {
                case "home":
                    LogTabEvent(Constants.Analytics.Target.Home);
                    break;
                case "trips":
                    LogTabEvent(Constants.Analytics.Target.Trips);
                    break;
                case "checkin":
                    LogTabEvent(Constants.Analytics.Target.CheckIn);
                    break;
                case "menu":
                    LogTabEvent(Constants.Analytics.Target.Menu);
                    break;
                default:
                    break;
            }
        }

        private void LogTabEvent(string target)
        {
            Mvx.IoCProvider.Resolve<IAnalyticsService>().LogEvent(Constants.Analytics.Events.NavTap, target);
        }

        private void HandleShowViewModelMessage(ShowViewModelMessage obj)
        {

            var index = GetViewModelIndex(obj.ViewModelType.Name);

            if (index > -1)
            {
                CurrentTabIndex = index;
            }
        }

        private int GetViewModelIndex(string viewModelName)
        {
            var result = -1;

            for (var i = 0; i < _viewModelTypes.Count; i++)
            {
                if (_viewModelTypes[i].Name == viewModelName)
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

        #endregion //Methods

        #region Commands

        public MvxAsyncCommand NavigateTabViewModelsCommand { get; }

        #endregion //Commands

        #region Commands Handlers

        private async Task DoNavigateTabViewModelsCommandAsync()
        {
            var tasks = _viewModelTypes.Select(x => NavigationService.Navigate(x));
            await Task.WhenAll(tasks);
        }

        #endregion //Commands Handlers
    }
}
