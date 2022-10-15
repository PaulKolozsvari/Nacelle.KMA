using System;
using MvvmCross.Plugin.Messenger;
using Nacelle.KMA.Core.Models.Entites;

namespace Nacelle.KMA.Core.Models.Messages
{
    public class PopulateTripsMessage : MvxMessage
    {
        public PopulateTripsMessage(object sender, BookingEntity bookingEntity) : base(sender)
        {
            Booking = bookingEntity;
        }

        public BookingEntity Booking { get; }
    }
}
