using System;
using System.Globalization;
using MvvmCross.Forms.Converters;
using Nacelle.KMA.Core.Models.Enums;

namespace Nacelle.KMA.UI.Converters
{
    public class SeatStatusTypeToResourcePathValueConverter: MvxFormsValueConverter<SeatStatus, string>
    {
        protected override string Convert(SeatStatus value, Type targetType, object parameter, CultureInfo culture)
        {
            const string basePath = "resource://Nacelle.KMA.UI.Resources.Images.";
            switch (value)
            {
                case SeatStatus.Available:
                    return $"{basePath}available-seat-inactive.svg";
                case SeatStatus.Selected:
                    return $"{basePath}selected-seat-active.svg";
                case SeatStatus.Traveller:
                    return $"{basePath}selected-seat-traveller.svg";
                case SeatStatus.Unavailable:
                    return $"{basePath}unavailable-seat.svg";
                default:
                    return $"{basePath}available-seat-inactive.svg";
            }
        }
    }
}
