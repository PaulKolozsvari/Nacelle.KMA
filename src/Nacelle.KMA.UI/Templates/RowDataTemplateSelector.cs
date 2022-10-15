using Nacelle.KMA.Core.Models.Items;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Templates
{
    public class RowDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate RowTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (!(item is RowItem row))
            {
                return new DataTemplate();
            }

            if (row.Row == 0)
            {
                return new DataTemplate();
            }

            row.SeatItems.Insert(0, new SeatItem
            {
                ColumnLetter = "L",
                Row = row.Row,
                IsExit = row.IsExitRow,
                IsRemoved = false
            });

            row.SeatItems.Add(new SeatItem
            {
                ColumnLetter = "R",
                Row = row.Row,
                IsExit = row.IsExitRow,
                IsRemoved = false
            });

            var aisle = row.SeatItems.Count / 2;

            row.SeatItems.Insert(aisle, new SeatItem
            {
                ColumnLetter = "M",
                Column = aisle,
                Row = row.Row,
                IsExit = row.IsExitRow,
                IsRemoved = false
            });

            return RowTemplate;
        }
    }
}
