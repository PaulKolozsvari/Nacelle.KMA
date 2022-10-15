using Nacelle.KMA.Core.Models.Items;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Templates
{
    public class SeatDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LeftWindowTemplate { get; set; }
        public DataTemplate RightWindowTemplate { get; set; }
        public DataTemplate LeftExitTemplate { get; set; }
        public DataTemplate RightExitTemplate { get; set; }
        public DataTemplate AisleTemplate { get; set; }
        public DataTemplate SeatTemplate { get; set; }
        public DataTemplate RemovedSeatTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (!(item is SeatItem seatItem))
            {
                return new DataTemplate();
            }

            // need to adjust the column refs for seats in cols 4 - 6
            if (seatItem.Column >= 4 && seatItem.Column <= 6)
            {
                seatItem.Column++;
            }

            if (seatItem.IsRemoved)
            {
                return RemovedSeatTemplate;
            }

            if (seatItem.ColumnLetter == "L")
            {
                return seatItem.IsExit ? LeftExitTemplate : LeftWindowTemplate;
            }

            if (seatItem.ColumnLetter == "R")
            {
                return seatItem.IsExit ? RightExitTemplate : RightWindowTemplate;
            }

            if (seatItem.ColumnLetter == "M")
            {
                return AisleTemplate;
            }

            return SeatTemplate;
        }
    }
}
