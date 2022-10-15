using System.Diagnostics;
using Nacelle.KMA.API.Models.Responses;
using Nacelle.KMA.Core.Mappers;
using Nacelle.KMA.Core.Repositories;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Nacelle.KMA.Core.Managers
{
    public class DataMigrationManager : IDataMigrationManager
    {
        private readonly IBookingRepository _bookingRepository;

        public DataMigrationManager(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task MigrateDataAsync()
        {
            await MigrateBookingsAsync();
            await MigrateReservationsAsync();
        }

        private string StoreName => DeviceInfo.Platform == DevicePlatform.Android ? "NativeStorage" : null;

        private async Task MigrateBookingsAsync()
        {
            const string key = "kma:bookings";
            if (Preferences.ContainsKey(key, StoreName))
            {
                var data = Preferences.Get(key, defaultValue: null, sharedName: StoreName);
                if (data != null && data != "null")
                {
                    data = SanitizeData(data);
                    var bookings = JsonConvert.DeserializeObject<List<Booking>>(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    Debug.WriteLine($"Found {bookings.Count} bookings to migrate");
                    if (bookings != null)
                    {
                        foreach (var booking in bookings)
                        {
                            var response = new RetrieveBookingIXResponse
                            {
                                Bookings = new Bookings()
                            };
                            response.Bookings.Booking = new List<Booking>(new[] { booking });
                            response.Bookings.Count = bookings.Count;
                            var bookingEntity = response.ToBookingEntity();
                            await _bookingRepository.AddBooking(bookingEntity);
                            Debug.WriteLine("Migrated booking: " + bookingEntity.RecordLocator);
                        }
                    }
                }

                Preferences.Remove(key);
            }
        }

        private async Task MigrateReservationsAsync()
        {
            const string key = "kma:reservations";
            if (Preferences.ContainsKey(key, StoreName))
            {
                var data = Preferences.Get(key, defaultValue: null, sharedName: StoreName);
                if (data != null && data != "null")
                {
                    data = SanitizeData(data);
                    var reservations = JsonConvert.DeserializeObject<List<ReservationData>>(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    Debug.WriteLine($"Found {reservations.Count} reservations to migrate");
                    if (reservations != null)
                    {
                        foreach (var reservation in reservations)
                        {
                            var response = new FindBookingResponse
                            {
                                Reservation = reservation.Data
                            };
                            var bookingEntity = response.ToBookingEntity();
                            await _bookingRepository.AddBooking(bookingEntity);
                            Debug.WriteLine("Migrated reservation: " + bookingEntity.RecordLocator);
                        }
                    }
                }
                Preferences.Remove(key);
            }
        }

        private static string SanitizeData(string data)
        {
            var c0 = "\\";
            var c1 = "\"";
            data = data.Replace(c0 + c1, c1);
            data = data.Trim('"');
            return data;
        }

        public class ReservationData
        {
            [JsonProperty("data")]
            public Reservation Data { get; set; }
        }
    }
}
