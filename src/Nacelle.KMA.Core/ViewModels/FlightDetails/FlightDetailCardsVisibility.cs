#region Using Directives

using Nacelle.KMA.Core.Enums;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels.FlightDetails
{
    public class FlightDetailCardsVisibility
    {
        #region Constructors

        public FlightDetailCardsVisibility(bool hasCheckedIn, bool canCheckIn) : this(TripType.None, hasCheckedIn, canCheckIn)
        {
        }

        public FlightDetailCardsVisibility(TripType tripType, bool hasCheckedIn, bool canCheckIn)
        {
            TripType = tripType;
            SetVisibility(tripType, hasCheckedIn, canCheckIn);
        }

        #endregion //Constructors

        #region Properties

        public TripType TripType { get; set; }

        public bool IsWeatherCardVisible { get; set; }

        public bool IsCheckInDateTimeCardViewVisible { get; set; }

        public bool IsBoardingDateTimeCardViewVisibleVisible { get; set; }

        public bool IsFlightLeavesSoonDateTimeCardViewVisible { get; set; }

        public bool IsDepartingDateTimeCardViewVisible { get; set; }

        public bool IsDelayedDateTimeCardViewVisible { get; set; }

        public bool IsCancelledDateTimeCardViewVisible { get; set; }

        #region Checkin Card View

        public bool IsCheckInCardViewVisible { get; set; }

        public bool IsCheckInCardCheckInCaptionVisible { get; set; }

        public bool IsCheckInCardFutureFlightLabelVisible { get; set; }

        public bool IsCheckInCardCheckInOpenLabelVisible { get; set; }

        public bool IsCheckInCardCheckInButtonVisible { get; set; }

        #endregion //Checkin Card View

        public bool IsPassengerSeatCardViewVisible { get; set; }

        public bool IsPassengerSeatCardBoardingPassButtonVisible { get; set; }

        public bool IsGateCardViewVisible { get; set; }

        #region Trip Card

        public bool IsTripCardTripsVisible { get; set; }

        public bool IsTripCardCheckInButtonVisible { get; set; }

        public bool IsTripCardViewBoardingPassIsVisible { get; set; }

        #endregion //Trip Card

        #endregion //Properties

        #region Fatory Methods

        public void SetVisibility(TripType tripType, bool hasCheckedIn, bool canCheckIn)
        {
            switch (tripType)
            {
                case TripType.None:
                    SetNoneVisibility(hasCheckedIn, canCheckIn);
                    break;
                case TripType.Future:
                    SetFutureVisibility(hasCheckedIn, canCheckIn);
                    break;
                case TripType.CheckInDayApproaching:
                    SetCheckInDayApproachingVisibility(hasCheckedIn, canCheckIn);
                    break;
                case TripType.CheckInDay:
                    SetCheckInDayVisibility(hasCheckedIn, canCheckIn);
                    break;
                case TripType.Boarding:
                    SetBoardingVisibility(hasCheckedIn, canCheckIn);
                    break;
                case TripType.LeavingSoon:
                    SetLeavingSoonVisibility(hasCheckedIn, canCheckIn);
                    break;
                case TripType.Departing:
                    SetDepartingVisibility(hasCheckedIn, canCheckIn);
                    break;
                case TripType.Past:
                    SetPastPastVisibility(hasCheckedIn, canCheckIn);
                    break;
                case TripType.Delayed:
                    SetDelayedVisibility(hasCheckedIn, canCheckIn);
                    break;
                case TripType.Cancelled:
                    SetCancelledVisibility(hasCheckedIn, canCheckIn);
                    break;
                default:
                    SetNoneVisibility(hasCheckedIn, canCheckIn);
                    break;
            }
        }

        public void SetNoneVisibility(bool hasCheckedIn, bool canCheckIn)
        {
        }

        public void SetFutureVisibility(bool hasCheckedIn, bool canCheckIn)
        {
            IsCheckInDateTimeCardViewVisible = true;

            #region Checkin Card

            IsCheckInCardViewVisible = true;
            IsCheckInCardCheckInCaptionVisible = true;
            IsCheckInCardFutureFlightLabelVisible = true;

            #endregion //Checkin Card

            //IsBoardingDateTimeCardViewVisibleVisible = hasCheckedIn;
            IsGateCardViewVisible = hasCheckedIn;
            IsPassengerSeatCardViewVisible = hasCheckedIn;
        }

        public void SetCheckInDayApproachingVisibility(bool hasCheckedIn, bool canCheckIn)
        {
            IsCheckInDateTimeCardViewVisible = true;

            #region Checkin Card

            IsCheckInCardViewVisible = true;
            IsCheckInCardCheckInCaptionVisible = true;
            IsCheckInCardFutureFlightLabelVisible = true;

            #endregion //Checkin Card

            IsGateCardViewVisible = hasCheckedIn;
            IsPassengerSeatCardViewVisible = hasCheckedIn;
        }

        public void SetCheckInDayVisibility(bool hasCheckedIn, bool canCheckIn)
        {
            IsCheckInDateTimeCardViewVisible = !hasCheckedIn && canCheckIn;

            #region Checkin Card

            IsCheckInCardViewVisible = !hasCheckedIn && canCheckIn;
            IsCheckInCardCheckInCaptionVisible = !hasCheckedIn && canCheckIn;
            IsCheckInCardCheckInOpenLabelVisible = !hasCheckedIn && canCheckIn;
            IsCheckInCardCheckInButtonVisible = !hasCheckedIn && canCheckIn;

            #endregion //Checkin Card

            IsBoardingDateTimeCardViewVisibleVisible = hasCheckedIn;
            IsGateCardViewVisible = hasCheckedIn;
            IsPassengerSeatCardViewVisible = hasCheckedIn;
            IsPassengerSeatCardBoardingPassButtonVisible = hasCheckedIn;

            IsWeatherCardVisible = true;
        }

        public void SetBoardingVisibility(bool hasCheckedIn, bool canCheckIn)
        {
            IsBoardingDateTimeCardViewVisibleVisible = true;
            IsGateCardViewVisible = hasCheckedIn;
            IsPassengerSeatCardViewVisible = hasCheckedIn;
        }

        public void SetLeavingSoonVisibility(bool hasCheckedIn, bool canCheckIn)
        {
            IsFlightLeavesSoonDateTimeCardViewVisible = true;
            IsGateCardViewVisible = hasCheckedIn;
            IsPassengerSeatCardViewVisible = hasCheckedIn;
        }

        public void SetDepartingVisibility(bool hasCheckedIn, bool canCheckIn)
        {
            IsDepartingDateTimeCardViewVisible = true;
            IsGateCardViewVisible = hasCheckedIn;
            IsPassengerSeatCardViewVisible = hasCheckedIn;
        }

        public void SetPastPastVisibility(bool hasCheckedIn, bool canCheckIn)
        {
            IsPassengerSeatCardViewVisible = hasCheckedIn;
            IsTripCardTripsVisible = true;
            IsTripCardCheckInButtonVisible = !hasCheckedIn & canCheckIn;
            IsTripCardViewBoardingPassIsVisible = hasCheckedIn;
        }

        public void SetDelayedVisibility(bool hasCheckedIn, bool canCheckIn)
        {
            IsDelayedDateTimeCardViewVisible = true;
            IsCheckInCardCheckInButtonVisible = !hasCheckedIn && canCheckIn;
            IsCheckInCardViewVisible = IsCheckInCardCheckInButtonVisible;
            IsCheckInCardCheckInCaptionVisible = !hasCheckedIn && canCheckIn;
            IsGateCardViewVisible = hasCheckedIn;
            IsPassengerSeatCardViewVisible = hasCheckedIn;
            IsPassengerSeatCardBoardingPassButtonVisible = hasCheckedIn;

            IsWeatherCardVisible = true;
        }

        public void SetCancelledVisibility(bool hasCheckedIn, bool canCheckIn)
        {
            IsCancelledDateTimeCardViewVisible = true;
            IsCheckInCardCheckInButtonVisible = !hasCheckedIn && canCheckIn;
            IsCheckInCardViewVisible = IsCheckInCardCheckInButtonVisible;
            IsCheckInCardCheckInCaptionVisible = !hasCheckedIn && canCheckIn;
            IsGateCardViewVisible = hasCheckedIn;
            IsPassengerSeatCardViewVisible = hasCheckedIn;
            IsPassengerSeatCardBoardingPassButtonVisible = hasCheckedIn;

            IsWeatherCardVisible = true;
        }

        #endregion //Fatory Methods
    }
}
