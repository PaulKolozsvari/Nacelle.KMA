using System;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.Commands;
using Nacelle.KMA.Core.ViewModels;

namespace Nacelle.KMA.Core.Models.Items
{
    public class SegmentItem
    {
        private IMvxNavigationService _navigationService;

        public string RouteDescription { get; set; }
        public string FlightNo { get; set; }
        public string DateAndTime { get; set; }

        public IMvxAsyncCommand CheckInCommand => new MvxAsyncCommand(() => NavigationService.Navigate<CheckInViewModel>());
        public IMvxAsyncCommand ViewBoardingPass => new MvxAsyncCommand(() => throw new NotImplementedException());

        private IMvxNavigationService NavigationService
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

        public bool HasCheckdIn { get; set; }
    }
}
