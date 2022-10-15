#region Using Directives

using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Plugin.Messenger;
using Nacelle.KMA.Core.Managers.Contracts;
using Nacelle.KMA.Core.Platform;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public abstract class RefreshableAndClosableViewModel : RefreshableViewModel
    {
        public RefreshableAndClosableViewModel(
            IConnectivityManager connectivityManager,
            IToastService toastService,
            IMvxMessenger mvxMessenger) : base(connectivityManager, toastService, mvxMessenger)
        {
            CloseCommand = new MvxAsyncCommand(DoClose);
        }

        #region Commands

        public ICommand CloseCommand { get; }

        #endregion //Commands

        #region Command Handlers

        private async Task DoClose()
        {
            await NavigationService.Close(this);
        }

        #endregion //Command Handlers
    }
}
