#region Using Directives

using System;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public interface IBookingQuery
    {
        #region Properties

        string BookingReference { get; set; }

        string LastName { get; set; }

        #endregion //Properties
    }
}
