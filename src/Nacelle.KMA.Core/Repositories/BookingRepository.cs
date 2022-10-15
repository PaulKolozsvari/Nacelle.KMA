#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nacelle.KMA.Core.Caching;
using Nacelle.KMA.Core.Models.Entites;
using Nacelle.KMA.Core.Types;

#endregion //Using Directives

namespace Nacelle.KMA.Core.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        #region Constructors

        public BookingRepository(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        #endregion //Constructors

        #region Fields

        private readonly ICacheService _cacheService;

        #endregion //Fields

        #region Methods

        public Task<BookingEntity> GetBookingAsync(string recordLocator)
        {
            var bookingEntities = _cacheService.GetValue<List<BookingEntity>>(EntityTypes.Bookings.ToString());
            var bookingEntity = bookingEntities.SingleOrDefault(x => x.RecordLocator.Equals(recordLocator, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(bookingEntity);
        }

        public Task<List<BookingEntity>> GetBookingsAsync()
        {
            return _cacheService.GetOrUpdateValue<List<BookingEntity>>(
                EntityTypes.Bookings.ToString(),
                () => Task.FromResult(new List<BookingEntity>()));
        }

        public Task SaveBookingsAsync(List<BookingEntity> bookingEntities)
        {
            _cacheService.SetValue<List<BookingEntity>>(EntityTypes.Bookings.ToString(), bookingEntities);
            return Task.FromResult(true);
        }

        public async Task RemoveBookingAsync(string recordLocator)
        {
            var bookings = await GetBookingsAsync();
            var filteredBookings = bookings.Where(x => !x.RecordLocator.Equals(recordLocator, StringComparison.OrdinalIgnoreCase)).ToList();
            await SaveBookingsAsync(filteredBookings);
        }

        public async Task AddBooking(BookingEntity bookingEntity)
        {
            var bookings = await GetBookingsAsync();
            // Make sure it isn't saved already by filtering the old record out, replace it with this new one
            var filteredBookings = bookings.Where(x => !x.RecordLocator.Equals(bookingEntity.RecordLocator, StringComparison.OrdinalIgnoreCase)).ToList();
            filteredBookings.Add(bookingEntity);
            await SaveBookingsAsync(filteredBookings);
        }

        public async Task<bool> ContainsBookingAsync(string recordLocator)
        {
            var bookings = await GetBookingsAsync();
            if (bookings == null)
            {
                return false;
            }
            var exists = bookings.Exists(x => x.RecordLocator.Equals(recordLocator, StringComparison.OrdinalIgnoreCase));
            return exists;
        }

        #endregion //Methods
    }
}
