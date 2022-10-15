#region Using Directives

using System.Collections.Generic;
using Nacelle.KMA.Core.Models.Enums;

#endregion //Using Directives

namespace Nacelle.KMA.Core.Models.Entites
{
    public class SeatMapEntity
    {
        #region Properties

        public CabinEntity Cabin { get; set; }

        #endregion //Properties
    }

    public class CabinEntity
    {
        #region Properties

        public string BookingClass { get; set; }

        public List<ColumnEntity> Columns { get; set; }

        public List<RowEntity> Rows { get; set; }

        #endregion //Properties
    }

    public class ColumnEntity
    {
        #region Properties

        public string Id { get; set; }

        public string Name { get; set; }

        public List<CharacteristicEntity> Characteristics { get; set; }

        #endregion //Properties
    }

    public class RowEntity
    {
        #region Properties

        public List<string> Characteristics { get; set; }

        public int Number { get; set; }

        public List<SlotEntity> Slots { get; set; }

        #endregion //Properties
    }

    public class SlotEntity
    {
        #region Properties

        public string ColumnRef { get; set; }

        public SeatEntity Seat { get; set; }

        #endregion //Properties
    }

    public class SeatEntity
    {
        #region Properties

        public string Number { get; set; }

        public SeatStatus Status { get; set; }

        public List<string> Limitations { get; set; }

        #endregion //Properties
    }
}
