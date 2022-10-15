using System;

namespace Nacelle.KMA.Core.Models.Items
{
    public class TripDateSectionHeaderItem : ITripItem
    {
        public TripDateSectionHeaderItem(string month, DateTime fromTime)
        {
            Month = month;
            FromTime = fromTime;
        }

        public string Month { get; }
        public DateTime FromTime { get; set; }
    }
}
