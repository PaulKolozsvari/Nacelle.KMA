#region Using Directives

using System.Collections.Generic;
using Nacelle.KMA.Core.Models.Items;
using Xamarin.Essentials;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        #region Constructors

        public MenuViewModel()
        {
            MenuItems = new List<BaseMenuItem>();
            PopulateMenuItems();
        }

        #endregion //Constructors

        #region Properties

        public List<BaseMenuItem> MenuItems { get; }

        #endregion //Properties

        #region Methods

        private void PopulateMenuItems()
        {
            MenuItems.Add(new MenuItemSeparator());
            MenuItems.Add(new MenuItem("Find a booking", "menu-search.svg", () => NavigationService.Navigate<FindBookingViewModel>(), Constants.Analytics.Target.FindABooking));
            MenuItems.Add(new MenuItemSeparator());
            MenuItems.Add(new MenuItem("Book a flight", "menu-book-flight.svg", () => NavigationService.Navigate<FlightBookingViewModel>(), Constants.Analytics.Target.BookAFlight));
            MenuItems.Add(new MenuItem("Add flight extras", "menu-cart.svg", () => NavigationService.Navigate<FlightExtrasViewModel>(), Constants.Analytics.Target.AddExtrasToYourFlight));
            MenuItems.Add(new MenuItem("Manage booking", "menu-edit.svg", () => NavigationService.Navigate<ManageBookingViewModel>(), Constants.Analytics.Target.ManageYourBooking));
            MenuItems.Add(new MenuItemSeparator());
            MenuItems.Add(new MenuItem("FAQ", "menu-help.svg", () => NavigationService.Navigate<FaqMenuViewModel>(), Constants.Analytics.Target.FAQ));
            MenuItems.Add(new MenuItem("Contact us", "menu-contact-us.svg", () => NavigationService.Navigate<ContactUsViewModel>(), Constants.Analytics.Target.ContactUs));
            MenuItems.Add(new MenuItem("Send feedback", "menu-chat.svg", () => Browser.OpenAsync(Constants.FeedbackURL, BrowserLaunchMode.SystemPreferred), Constants.Analytics.Target.SendFeedback));
            MenuItems.Add(new MenuItemSeparator());
            MenuItems.Add(new MenuItem("www.kulula.com", "menu-globe.svg", () => Browser.OpenAsync(Constants.KululaURL, BrowserLaunchMode.SystemPreferred), Constants.Analytics.Target.KululaWebsite));
            MenuItems.Add(new MenuItem("About the app", "menu-app.svg", () => NavigationService.Navigate<AboutViewModel>(), Constants.Analytics.Target.About));
        }

        #endregion //Methods
    }
}
