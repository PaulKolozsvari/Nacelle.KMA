#region Using Directives

using System.Collections.Generic;
using Nacelle.KMA.Core.Models;
using Nacelle.KMA.Core.Models.Items;

#endregion //Using Directives

namespace Nacelle.KMA.Core.NavBundles
{
    public class CheckInNavBundle: ReferenceData
    {
        #region Properties

        public List<CheckInItem> CheckInItems { get; set; }

        #endregion //Properties
    }

    public class SelectSeatCheckInNavBundle: ReferenceData
    {
        #region Constructors

        public SelectSeatCheckInNavBundle(CheckInNavBundle checkInNavBundle)
        {
            ConversationID = checkInNavBundle.ConversationID;
            BookingReference = checkInNavBundle.BookingReference;
            LastName = checkInNavBundle.LastName;
        }

        #endregion //Constructors

        #region Properties

        public TravellerSelectSeatItem SelectedTraveller { get; set; }
        public string SelectedSegmentId { get; set; }
        public List<TravellerSelectSeatItem> TravellerItems { get; set; }

        #endregion //Properties
    }

    public class CheckedInNavBundle : CheckInNavBundle
    {
        #region Constructors

        public CheckedInNavBundle() { }

        public CheckedInNavBundle(CheckInNavBundle checkInNavBundle)
        {
            ConversationID = checkInNavBundle.ConversationID;
            BookingReference = checkInNavBundle.BookingReference;
            LastName = checkInNavBundle.LastName;
            CheckInItems = checkInNavBundle.CheckInItems;
        }

        #endregion //Constructors

        #region Properties

        public List<BoardingPassItem> BoardingPassItems { get; set; }

        #endregion //Properties
    }
}
