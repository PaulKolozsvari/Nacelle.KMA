using System.Collections.Generic;
using System.Threading.Tasks;
using Nacelle.KMA.Core.Models.Entites;

namespace Nacelle.KMA.Core.Repositories
{
    public interface IBookingRepository
    {
        Task<BookingEntity> GetBookingAsync(string recordLocator);  
        Task<bool> ContainsBookingAsync(string recordLocator);
        Task AddBooking(BookingEntity bookingEntity);
        Task<List<BookingEntity>> GetBookingsAsync();
        Task RemoveBookingAsync(string recordLocator);
        Task SaveBookingsAsync(List<BookingEntity> bookingEntities);
    }
}