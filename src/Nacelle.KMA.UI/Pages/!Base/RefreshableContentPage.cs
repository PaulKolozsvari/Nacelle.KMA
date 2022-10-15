#region Using Directives

using MvvmCross;
using MvvmCross.Forms.Views;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using Nacelle.KMA.Core.Messages;
using Nacelle.KMA.Core.ViewModels;
using Refractored.XamForms.PullToRefresh;
using Xamarin.Essentials;
using Xamarin.Forms;

#endregion //Using Directives

namespace Nacelle.KMA.UI.Pages
{
    /// <summary>
    /// Uses IMvxMessenger to subscribe to and receive updates from RefreshableViewModels when the refresh has started or stopped.
    /// https://www.mvvmcross.com/documentation/plugins/messenger
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RefreshableContentPage<T> : BaseContentPage<T> where T : MvxViewModel
    {
        #region Constructors

        public RefreshableContentPage()
        {
            _mvxMessenger = Mvx.Resolve<IMvxMessenger>();
        }

        #endregion //Constructors

        #region Fields

        protected PullToRefreshLayout PullToRefresh { get; set; }
        protected IMvxMessenger _mvxMessenger;
        protected MvxSubscriptionToken _mvxSubscriptionToken;

        #endregion //Fields

        #region Methods

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SubscribeToViewModelMessages();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            UnsubscribeToViewModelMessages();
        }

        protected virtual void SubscribeToViewModelMessages()
        {
            _mvxSubscriptionToken = _mvxMessenger.Subscribe<RefreshStateMessage>(new System.Action<RefreshStateMessage>(OnRefreshStateMessage));
        }

        protected void OnRefreshStateMessage(RefreshStateMessage refreshStateMessage)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (PullToRefresh != null)
                {
                    if (refreshStateMessage.IsBusy && !PullToRefresh.IsRefreshing)
                    {
                        PullToRefresh.IsRefreshing = true;
                    }
                    else if(!refreshStateMessage.IsBusy && PullToRefresh.IsRefreshing)
                    {
                        PullToRefresh.IsRefreshing = false;
                    }
                }
            });
        }

        protected virtual void UnsubscribeToViewModelMessages()
        {
            if (_mvxSubscriptionToken != null)
            {
                _mvxMessenger.Unsubscribe<RefreshStateMessage>(_mvxSubscriptionToken);
            }
        }

        #endregion //Methods
    }
}
