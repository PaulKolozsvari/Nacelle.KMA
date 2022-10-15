using System;
namespace Nacelle.KMA.Core.Enums
{
    public enum TripType
    {
        None,
        Future,
        CheckInDayApproaching,
        CheckInDay,
        Boarding,
        LeavingSoon,
        Departing,
        Past,
        Delayed,
        Cancelled
    }
}
