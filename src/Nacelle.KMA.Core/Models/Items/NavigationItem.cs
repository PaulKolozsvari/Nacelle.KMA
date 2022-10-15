using System;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Nacelle.KMA.Core.Models.Items
{
    public class NavigationItem: MvxNotifyPropertyChanged
    {
        private IMvxNavigationService _navigationService;

        public IMvxNavigationService NavigationService
        { 
            get
            {
                if (_navigationService == null)
                {
                    _navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
                }

                return _navigationService;
            }
         }
    }
}
