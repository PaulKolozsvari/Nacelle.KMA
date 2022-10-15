#region Using Directives

using System;
using MvvmCross.ViewModels;
using Nacelle.KMA.API.Models.Enums;

#endregion //Using Directives

namespace Nacelle.KMA.Core.Models.Items
{
    public class TravellerSelectSeatItem : MvxNotifyPropertyChanged
    {
        #region Constructors

        public TravellerSelectSeatItem(TravellerItem travellerItem, string passengerFlightId, SeatItem seatItem)
        {
            Id = travellerItem.Id;
            FirstName = travellerItem.FirstName;
            LastName = travellerItem.LastName;
            Name = travellerItem.Name;
            PassengerFlightId = passengerFlightId;
            SeatItem = seatItem;
            Index = travellerItem.Index;
            PassengerType = travellerItem.PassengerType;
            HasInfant = travellerItem.HasInfant;
            InfantName = travellerItem.InfantName;
            InfantPassengerId = travellerItem.InfantId;
            HasInfantOrIsChild = HasInfant || PassengerType == PassengerTypes.Child;
        }

        #endregion //Constructors

        #region Fields

        private SeatItem _seatItem;

        #endregion //Fields

        #region Properties

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name { get; }

        public string InfantName { get; }

        public bool HasInfant { get; }

        public string InfantPassengerId { get; }

        public bool HasInfantOrIsChild { get; }

        public PassengerTypes PassengerType { get; }

        public int Index { get; set; }

        public string SegmentId { get; set; }

        public string PassengerFlightId { get; set; }

        public SeatItem SeatItem
        {
            get => _seatItem;
            set
            {
                if (SetProperty(ref _seatItem, value))
                {
                    RaisePropertyChanged(() => Seat);
                    RaisePropertyChanged(() => SeatDescription);
                }
            }
        }

        public string Seat => SeatItem == null ? string.Empty : SeatItem.Seat;

        public string SeatDescription => $"Seat {Seat} ({GetSeatLocation()})";

        #endregion //Properties

        #region Methods

        private string GetSeatLocation()
        {
            if (string.IsNullOrEmpty(Seat))
            {
                return "--";
            }
            if (Seat.EndsWith("A", StringComparison.InvariantCultureIgnoreCase) || Seat.EndsWith("F", StringComparison.InvariantCultureIgnoreCase))
            {
                return "window";
            }

            if (Seat.EndsWith("C", StringComparison.InvariantCultureIgnoreCase) || Seat.EndsWith("D", StringComparison.InvariantCultureIgnoreCase))
            {
                return "aisle";
            }

            return "middle";
        }

        #endregion //Methods
    }
}