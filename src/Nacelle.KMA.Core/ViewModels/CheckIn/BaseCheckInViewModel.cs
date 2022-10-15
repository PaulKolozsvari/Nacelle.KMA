#region Using Directives

using System;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Presenters.Hints;
using Nacelle.KMA.Core.NavBundles;
using Nacelle.KMA.Core.Platform;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public abstract class BaseCheckInViewModel : BaseViewModel<CheckInNavBundle>
    {
        #region Constructors

        protected BaseCheckInViewModel()
        {
            CancelCommand = new MvxAsyncCommand(DoCancel);
        }

        #endregion //Constructors

        #region Methods

        public MvxAsyncCommand CancelCommand { get; }

        private async Task DoCancel()
        {
            var alert = Mvx.IoCProvider.Resolve<IAlertService>();

            var yes = (Title: "Yes", Action: new Func<Task>(() => PopToRoot()));
            var no = (Title: "No", Action: new Func<Task>(() => Task.FromResult(false)));

            await alert.Show("Cancel check-in?",
                "All your check-in information and selections up to now will be lost. Are you sure you want to cancel check-in at this stage?",
                yes,
                no);
        }

        protected virtual Task PopToRoot()
        {
            return NavigationService.ChangePresentation(new MvxPopToRootPresentationHint());
        }

        #endregion //Methods
    }
}
