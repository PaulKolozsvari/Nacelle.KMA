#region Using Directives

using System.Diagnostics;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        #region Fields

        private readonly IMvxNavigationService _navigationService;
        private bool _isBusy;

        #endregion //Fields

        #region Properties

        public bool IsBusy { get => _isBusy; set => SetProperty(ref _isBusy, value); }

        protected IMvxNavigationService NavigationService => _navigationService ?? Mvx.IoCProvider.Resolve<IMvxNavigationService>();

        #endregion //Properties

        #region Methods

        public override Task Initialize()
        {
            Debug.WriteLine("Navigated to: " + this.GetType().Name);
            return base.Initialize();
        }

        #endregion //Methods
    }
}