#region Using Directives

using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Nacelle.KMA.Core.Models.Enums;

#endregion //Using Directives

namespace Nacelle.KMA.Core.Models.Items
{
    public class SeatItem : MvxNotifyPropertyChanged
    {
        #region Constructors

        public SeatItem()
        {
            SelectSeatCommand = new MvxCommand(DoSeatSelectCommand, CanSelectSeat);
        }

        public SeatItem(string seat) : this()
        {
            if (!string.IsNullOrEmpty(seat))
            {
                var column = seat[seat.Length - 1];
                if (int.TryParse(seat.TrimEnd(column), out var row))
                {
                    Row = row;
                    ColumnLetter = column.ToString();
                }
            }
        }

        #endregion //Constructors

        #region Fields

        private SeatStatus _seatStatus;
        private string _overlayText;

        #endregion //Fields

        private bool CanSelectSeat() =>
            (SeatStatus != SeatStatus.Unavailable) &&
            (SeatStatus != SeatStatus.Traveller);

        #region Properties

        public int Row { get; set; }

        public int Column { get; set; }

        public string ColumnLetter { get; set; }

        public string Seat => Row == 0 ? string.Empty : $"{Row}{ColumnLetter}";

        public bool IsExit { get; set; }

        public bool IsRemoved { get; set; }

        public bool IsFront { get; set; }

        public SeatStatus SeatStatus { get => _seatStatus; set => SetProperty(ref _seatStatus, value); }

        public string Label { get => _overlayText; set => SetProperty(ref _overlayText, value); }

        #endregion //Properties

        #region Commands

        public MvxCommand SelectSeatCommand { get; }

        #endregion //Commands

        #region Command Handlers

        private void DoSeatSelectCommand()
        {
            if (SeatStatus == SeatStatus.Selected)
            {
                SeatStatus = SeatStatus.Available;
            }
            else if (SeatStatus == SeatStatus.Available)
            {
                SeatStatus = SeatStatus.Selected;
            }
        }

        #endregion //Command Handlers
    }
}
