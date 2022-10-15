#region Using Directives

using System.Threading.Tasks;
using MvvmCross.Commands;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public class ExitRowTermsViewModel : WebPageViewModel
    {
        #region Constructors

        public ExitRowTermsViewModel()
        {
            CancelCommand = new MvxAsyncCommand(DoCancelCommand);
            AcceptCommand = new MvxAsyncCommand(DoAcceptCommand);
        }

        #endregion //Constructors

        #region Fields

        private bool _isTermsAccepted;

        #endregion //Fields

        #region Properties

        public bool IsTermsAccepted { get => _isTermsAccepted; set => SetProperty(ref _isTermsAccepted, value); }

        #endregion //Properties

        #region Commands

        public MvxAsyncCommand CancelCommand { get; }
        public MvxAsyncCommand AcceptCommand { get; }

        #endregion //Commands

        #region Command Handlers

        private async Task DoAcceptCommand()
        {
            await NavigationService.Close(this, true);
        }

        private async Task DoCancelCommand()
        {
            await NavigationService.Close(this, false);
        }

        #endregion //Command Handlers
    }
}
