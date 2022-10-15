#region Using Directives

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AppCenter;
using MvvmCross.Commands;
using MvvmCross.Plugin.Messenger;
using Nacelle.KMA.Core.Managers.Contracts;
using Nacelle.KMA.Core.Messages;
using Nacelle.KMA.Core.Platform;
using Xamarin.Essentials;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public abstract class RefreshableViewModel  : BaseViewModel
    {
        #region Constructors

        public RefreshableViewModel(IConnectivityManager connectivityManager, IToastService toastService, IMvxMessenger mvxMessenger)
        {
            ToastService = toastService;
            ConnectivityManager = connectivityManager;
            RefreshCommand = new MvxAsyncCommand(DoRefreshCommandAsync);
            _mvxMessenger = mvxMessenger;
        }

        #endregion //Constructors

        #region Fields

        internal readonly IConnectivityManager ConnectivityManager;
        internal readonly IToastService ToastService;
        private IMvxMessenger _mvxMessenger;

        #endregion //Fields

        #region Commands

        public MvxAsyncCommand RefreshCommand { get; }

        #endregion //Commands

        #region Methods

        public abstract Task RefreshFunc();

        private async Task DoRefreshCommandAsync()
        {
            try
            {
                if (ConnectivityManager.NetworkAccess == Enums.NetworkAccess.None)
                {
                    await ToastService.Show(Constants.Messages.NoInternetConnectdion, true);
                    return;
                }
                if (IsBusy)
                {
                    return;
                }
                IsBusy = true;
                _mvxMessenger.Publish(new RefreshStateMessage(this, true));

                //await BookingManager.RefreshAllBookingsAsync();
                await RefreshFunc();
            }
            catch (Exception ex)
            {
                // Todo: Display error dialog
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                _mvxMessenger.Publish(new RefreshStateMessage(this, false));
                IsBusy = false;
            }
        }

        #endregion //Methods
    }
}
