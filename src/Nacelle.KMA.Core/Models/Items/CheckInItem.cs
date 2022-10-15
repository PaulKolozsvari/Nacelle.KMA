#region Using Directives

using System.Collections.Generic;

#endregion //Using Directives

namespace Nacelle.KMA.Core.Models.Items
{
    public class CheckInItem
    {
        #region Properties

        public List<BookingItem> BookingItems { get; set; }
        public List<TravellerItem> TravellerItems { get; set; }

        #endregion //Properties
    }
}
