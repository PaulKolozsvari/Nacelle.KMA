#region Using Directives

using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public abstract class BaseClosableViewModel : BaseViewModel
    {
        #region Constructors

        protected BaseClosableViewModel()
        {
            CloseCommand = new MvxAsyncCommand(DoClose);
        }

        #endregion //Constructors

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
