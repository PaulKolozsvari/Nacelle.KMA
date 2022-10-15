namespace Nacelle.KMA.API.Exceptions.Factories
{
    public static class ExceptionFactory
    {
        public static class General
        {
            public static GeneralException SomethingWentWrong()
            {
                return new GeneralException("Something went wrong"); 
            }

            public static GeneralException WithMessage(string message)
            {
                return new GeneralException("Something went wrong: " + message);
            }

            public static GeneralException TechnicalDifficulties()
            {
                return new GeneralException("We seem to be experiencing some technical difficulties, please try again later");
            }
        }

        public static class FindBooking
        {
            public static FindBookingException NotFound(string code, string type)
            {
                return new FindBookingException("Booking reference can not be found", code, type);
            }

            public static FindBookingException NotFound()
            {
                return new FindBookingException("Booking reference can not be found");
            }

            public static FindBookingException FlightNotInitialized(string code, string type)
            {
                return new FindBookingException("No kulula.com flights found, your flight may be operated by another airline.", code, type);
            }
        }

        public static class Booking
        {
            public static BookingException Exists()
            {
                return new BookingException("Booking already added as a trip, go to the My Trips section");
            }

            public static BookingException PastTrips()
            {
                return new BookingException("All flights in this booking have already departed");
            }
        }

        public static class BoardingPass
        {
            public static BoardingPassException NoCheckedInPassengers()
            {
                return new BoardingPassException("No checked in passengers to get boarding passes for on this flight.");
            }

            public static BoardingPassException InhibitedTimeRestriction(string code, string type)
            {
                return new BoardingPassException("Unable to retrieve boarding pass, please proceed to airport check-in counter.", code, type);
            }
        }

        public static class CheckIn
        {
            public static CheckInException IsInhibitedBySSR()
            {
                return new CheckInException("Unable to check-in, please check-in at the airport counter.");
            }

            public static CheckInException NotEligible()
            {
                return new CheckInException("This booking has no flight eligible for check in.");
            }

            public static CheckInException ReservationNotEligible()
            {
                return new CheckInException("Reservation is not eligible for check-in.");
            }

            public static CheckInException FirstSegmentException(string message)
            {
                return new CheckInException(message);
            }

            public static ExpiredSession ExpiredConversation()
            {
                return new ExpiredSession();
            }
        }
    }
}
