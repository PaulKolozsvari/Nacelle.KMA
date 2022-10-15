using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.ViewModels;
using Nacelle.Core.Models;
using Nacelle.KMA.Core.Models.Entites;
using Nacelle.KMA.Core.Models.Items;
using System;

namespace Nacelle.KMA.Core.Managers
{
    public interface IBookingManager
    {
        Task<IEnumerable<BookingEntity>> LoadBookingsAsync();
        Task<BookingEntity> LoadBookingAsync(string recordLocator);
        Task<Response<BookingEntity>> QueryBookingAsync(string recordLocator, string lastName);
        Task<Response<BookingEntity>> FindBookingAsync(string recordLocator, string lastName);
        Task<Response<BookingEntity>> FindAndSaveBookingAsync(string recordLocator, string lastName);
        Task<Response<BookingEntity>> FindAndSaveBookingAsync(string recordLocator, string lastName, bool validateCheckinEligibility);
        Task RefreshAllBookingsAsync();
        Task<Response<BookingEntity>> RefreshBookingAsync(BookingEntity bookingEntity);
        Task RemoveBooking(string recordLocator);
        Task<FlightStatusItem> GetFlightStatus(DateTime departureDate, string departureCode, string flightNumber);
        Task<string> RefreshConversationID(string recordLocator, string lastName);

        MvxObservableCollection<BookingItem> BookingItems { get; }
        MvxObservableCollection<ITripItem> TripItems { get; }

        Task PopulateBookingsAsync();
    }
}