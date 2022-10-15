#region Properties

using System;
using System.Collections.Generic;
using Nacelle.KMA.API.Models.Responses;

#endregion //Properties

namespace Nacelle.KMA.Core.Models.Entites
{
    /// <summary>
    /// Used as as DB table class for storing a collection of BookingEntity
    /// </summary>
    public class BookingListEntity
    {
        #region Properties

        // Used to combine the RetrieveBookingIXResponse and FindBookingResponse into a single domain entity class
        //public List<BookingEntity> Bookings { get; set; }

        // Torn between using raw response or combining two responss into a local domaine entity object as above
        public List<RetrieveBookingIXResponse> AllBookings { get; set; }
        public List<FindBookingResponse> Bookings { get; set; }

        #endregion //Properties
    }
}
