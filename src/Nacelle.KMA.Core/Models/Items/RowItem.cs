using System.Collections.Generic;

namespace Nacelle.KMA.Core.Models.Items
{
    public class RowItem
    {
        public List<SeatItem> SeatItems { get; set; }
        public int Row { get; set; }
        public bool IsExitRow { get; set; }

        public override string ToString()
        {
            return "Row No: " + Row;
        }
    }
}
