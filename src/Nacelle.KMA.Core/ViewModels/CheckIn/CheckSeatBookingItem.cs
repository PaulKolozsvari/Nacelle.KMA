#region Using Directives

using System;
using System.Collections.Generic;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Nacelle.KMA.Core.Models.Items;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public class CheckSeatBookingItem : MvxNotifyPropertyChanged
    {
        #region Constructors

        public CheckSeatBookingItem()
        {
            SelectSeatCommand = new MvxCommand<TravellerSelectSeatItem>(SelectSeat);
        }

        #endregion //Constructors

        #region Events

        public event EventHandler<TravellerSelectSeatItem> TravellerSelected;

        #endregion //Events

        #region Fields

        private bool _seatUpdated;

        #endregion //Fields

        #region Properties

        public string From { get; set; }

        public string To { get; set; }

        public string SegmentId { get; set; }

        public List<TravellerSelectSeatItem> TravellerItems { get; set; }

        public bool SeatUpdated
        {
            get => _seatUpdated;
            set => SetProperty(ref _seatUpdated, value);
        }

        #endregion //Properties

        #region Commands

        public MvxCommand<TravellerSelectSeatItem> SelectSeatCommand { get; }

        #endregion //Commands

        #region Methods

        private void SelectSeat(TravellerSelectSeatItem traveller)
        {
            TravellerSelected?.Invoke(this, traveller);
        }

        #endregion //Methods
    }
}
