namespace Nacelle.KMA.Core
{
    public static class Constants
    {
        public static readonly string KululaURL = "https://kulula.com";
        public static readonly string KululaManageBookingURL = "https://www.kulula.com/manage-booking";
        public static readonly string KululaPhonePrefix = "0861";
        public static readonly string KululaPhoneSuffix = "585852";
        public static readonly string KululaPhoneNo = KululaPhonePrefix + KululaPhoneSuffix;
        public static readonly string KululaAlphaPhoneNo = $"CALL {KululaPhonePrefix} KULULA({KululaPhoneSuffix})";
        public static readonly string FeedbackURL = "https://a.pgtb.me/zmJjGC";
        public static readonly string KululaEmail = "feedback@kulula.com";
        public static readonly string HtmlFormatString = "<html><head><style>* {{ font-family: 'Open Sans', Fallback, sans-serif; color: #4f4f4f; font-size: 12pt}} ul li {{ padding-bottom: 8px; }}</style></head><body>{0}</body></html>";
        public static readonly string KululaTermsUrl = "https://www.kulula.com/general/legal/online-check-in";
        public static readonly string MonkeyCacheApplicationId = "com.nacelle.kulula.LiteDB";

        public static class ContextActions
        {
            public const string AddExtras = "Add extras";
            public const string BookAFlight = "Book a flight";
            public const string FindABooking = "Find a booking";
            public const string RemoveBooking = "Remove from app";
            public const string ManageBooking = "Manage booking";
        }

        public static class Messages
        {
            public const string NoInternetConnectdion = "You’re offline, please check your internet connection";
        }

        public static class Text
        {
            public const string OK = "OK";
        }

        public static class Analytics
        {
            public static class Events
            {
                public static readonly string ButtonTap = "button_tap";
                public static readonly string CardTap = "card_tap";
                public static readonly string MenuItemTap = "menu_item_tap";
                public static readonly string NavTap = "nav_tap";
                public static readonly string FAQItemTap = "faq_item_tap";
                public static readonly string OptionsItemTap = "options_item_tap";
                public static readonly string FindBookinUserError = "find_booking_user_error";
                public static readonly string BoardingPassShare = "boarding_pass_share";
                public static readonly string Online = "online";
                public static readonly string Offline = "offline";
            }

            public static class Target
            {
                public static readonly string BookAFlight = "book_a_flight";
                public static readonly string FindMyBooking = "find_my_booking";
                public static readonly string FindABooking = "find_a_booking";
                public static readonly string CheckInNow = "check-in_now";
                public static readonly string ViewBoardingPass = "view_boarding_pass";
                public static readonly string ExtrasCard = "extras_card";
                public static readonly string FlightCard = "flight_card";
                public static readonly string NoThanks = "no-thanks";
                public static readonly string Continue = "continue";
                public static readonly string AddExtrasToYourFlight = "add_extras_to_your_flight";
                public static readonly string ManageYourBooking = "manage_your_booking";
                public static readonly string FAQ = "faq";
                public static readonly string ContactUs = "contact-us";
                public static readonly string SendFeedback = "send-feedback";
                public static readonly string Settings = "settings";
                public static readonly string KululaWebsite = "www.kulula.com";
                public static readonly string About = "about";
                public static readonly string PrivacyPolicy = "privacy_policy";
                public static readonly string TandC = "terms_&_conditions";
                public static readonly string PurchaseOnline = "purchase_online";
                public static readonly string Phone = "phone:_0861_kulula_(585852)";
                public static readonly string ManageBookingsOnline = "manage_bookings_online";
                public static readonly string Home = "home";
                public static readonly string Trips = "trips";
                public static readonly string CheckIn = "check-in";
                public static readonly string Menu = "menu";
                public static readonly string ManageBooking = "manage_booking";
                public static readonly string AddExtras = "add_extras";
                public static readonly string RemoveFromApp = "remove_from_app";
            }

            public static class Context
            {
                public static readonly string BookingReference = "context_booking_reference";
                public static readonly string FlightNumber = "context_flight_number";
                public static readonly string Origin = "context_origin";
                public static readonly string Destination = "context_destination";
                public static readonly string DepartureDate = "context_departure_date";
                public static readonly string DepartureTime = "context_departure_time";
                public static readonly string ShareResult = "context_share_result";
            }
        }
    }
}
