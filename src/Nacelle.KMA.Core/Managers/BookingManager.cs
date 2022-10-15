#region Using Directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.ViewModels;
using Nacelle.Core.Models;
using Nacelle.KMA.API.Models.Requests;
using Nacelle.KMA.API.Services;
using Nacelle.KMA.API.ExtensionMethods;
using Nacelle.KMA.Core.Caching;
using Nacelle.KMA.Core.ExtensionMethods;
using Nacelle.KMA.Core.Mappers;
using Nacelle.KMA.Core.Models.Entites;
using Nacelle.KMA.Core.Models.Items;
using Nacelle.KMA.Core.Repositories;
using Nacelle.KMA.API.Exceptions.Factories;
using MvvmCross.Logging;
using Nacelle.KMA.API.Exceptions;
using Nacelle.KMA.API.Models.Responses;

#endregion //Using Directives

namespace Nacelle.KMA.Core.Managers
{
    public class BookingManager : MvxNotifyPropertyChanged, IBookingManager
    {
        public BookingManager(
            IBookingRepository bookingRepository,
            ITibcoService tibcoService,
            IOpsApiService opsApiService,
            IAppSettings appSettings)
        {
            _opsApiService = opsApiService;
            _bookingRepository = bookingRepository;
            _tibcoService = tibcoService;
            _appSettings = appSettings;
            BookingItems = new MvxObservableCollection<BookingItem>();
            TripItems = new MvxObservableCollection<ITripItem>();
        }

        #region Fields

        private readonly IBookingRepository _bookingRepository;
        private readonly ITibcoService _tibcoService;
        private readonly IAppSettings _appSettings;
        private readonly IOpsApiService _opsApiService;

        #endregion //Fields

        #region Properties

        public MvxObservableCollection<BookingItem> BookingItems { get; }
        public MvxObservableCollection<ITripItem> TripItems { get; }

        #endregion //Properties

        #region Methods

        public async Task<IEnumerable<BookingEntity>> LoadBookingsAsync() => await _bookingRepository.GetBookingsAsync();
        public Task<BookingEntity> LoadBookingAsync(string recordLocator) => _bookingRepository.GetBookingAsync(recordLocator);

        public async Task<Response<BookingEntity>> RefreshBookingAsync(BookingEntity bookingEntity)
        {
            var recordLocator = bookingEntity.RecordLocator;
            var lastName = bookingEntity.Passengers.First().LastName;
            Response<BookingEntity> result = await QueryBookingAsync(recordLocator, lastName);
            return result;
        }

        public async Task<Response<BookingEntity>> QueryBookingAsync(string recordLocator, string lastName)
        {
            Response<BookingEntity> bookingEntity = await RetrieveBookingAsync(recordLocator, lastName);
            if (!bookingEntity.IsSuccess)
            {
                return bookingEntity;
            }
            await _bookingRepository.AddBooking(bookingEntity.Data); //Save the retrieve booking entity.
            if (bookingEntity.Data.Passengers.Any() && (bookingEntity.Data.Passengers.First().Segments.Any(x => (x.FromTime - DateTime.Now).IsIn24HourWindow())))
            {
                try
                {
                    Response<BookingEntity> findBookingEntity = await FindBookingAsync(recordLocator, lastName);
                    if (findBookingEntity.IsSuccess)
                    {
                        MergeBookingInfo(bookingEntity.Data, findBookingEntity.Data);
                        await _bookingRepository.AddBooking(bookingEntity.Data);
                    }
                }
                catch (FindBookingException)
                {
                    // It couldnt find the booking but it could be that its too far in the future, but it was obviously
                    // found by the retrieve booking , so let the normal program flow will follow.
                }
            }
            await PopulateBookingsAsync();
            return bookingEntity;
        }

        private void MergeBookingInfo(BookingEntity retrieveBookingEntity, BookingEntity findBookingEntity)
        {
            //Copy segments and passengers to retrieve booking entity from the find booking entity.
            retrieveBookingEntity.ConversationID = findBookingEntity.ConversationID;
            for (int i = 0; i < retrieveBookingEntity.Passengers.Count; i++)
            {
                PassengerEntity passengerEntity = retrieveBookingEntity.Passengers[i];
                for (int j = 0; j < passengerEntity.Segments.Count; j++)
                {
                    SegmentEntity segmentEntity = passengerEntity.Segments[j];
                    SegmentEntity findBookingSegment = GetSegmentFromBookingEntity(findBookingEntity, passengerEntity.FirstName, passengerEntity.LastName, segmentEntity.FlightNumber);
                    if (findBookingSegment != null)
                    {
                        passengerEntity.Segments[j] = findBookingSegment; //Replace the segment on retrieve booking with the segment from find booking.
                    }
                }
            }
            for (int i = 0; i < retrieveBookingEntity.PassengerSegments.Count; i++)
            {
                SegmentEntity segmentEntity = retrieveBookingEntity.PassengerSegments[i];
                SegmentEntity findBookingSegment = GetPassengerSegmentFromEntity(findBookingEntity, segmentEntity.FirstName, segmentEntity.LastName, segmentEntity.FlightNumber);
                if (findBookingSegment != null)
                {
                    retrieveBookingEntity.PassengerSegments[i] = findBookingSegment;
                }
            }
        }

        private PassengerEntity GetPassengerFromBookingEntity(BookingEntity bookingEntity, string firstName, string lastName)
        {
            PassengerEntity result = bookingEntity.Passengers.Where(x => x.FirstName.Equals(firstName) && x.LastName.Equals(lastName)).FirstOrDefault();
            return result;
        }

        private SegmentEntity GetSegmentFromBookingEntity(BookingEntity bookingEntity, string firstName, string lastName, string flightNumber)
        {
            PassengerEntity passengerEntity = GetPassengerFromBookingEntity(bookingEntity, firstName, lastName);
            if (passengerEntity == null)
            {
                return null;
            }
            SegmentEntity result = passengerEntity.Segments.Where(x => x.FlightNumber.Equals(flightNumber)).FirstOrDefault();
            return result;
        }

        public SegmentEntity GetPassengerSegmentFromEntity(BookingEntity bookingEntity, string firstName, string lastName, string flightNumber)
        {
            SegmentEntity result = bookingEntity.PassengerSegments.Where(x => x.FirstName.Equals(firstName) && x.LastName.Equals(lastName) && x.FlightNumber.Equals(flightNumber)).FirstOrDefault();
            return result;
        }

        public async Task RefreshAllBookingsAsync()
        {
            try
            {
                List<BookingEntity> originalBookings = await _bookingRepository.GetBookingsAsync();
                if (originalBookings == null || !originalBookings.Any())
                {
                    return;
                }
                for (var i = 0; i < originalBookings.Count; i++)
                {
                    BookingEntity originalBooking = originalBookings[i];
                    if (!originalBooking.PassengerSegments.Any(x => x.FromTime > DateTime.Now))
                    {
                        continue;
                    }
                    try
                    {
                        var refreshedBookingResponse = await RefreshBookingAsync(originalBooking);
                        if (refreshedBookingResponse.IsSuccess)
                        {
                            originalBookings[i] = refreshedBookingResponse.Data;
                            await _bookingRepository.AddBooking(refreshedBookingResponse.Data);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Why coding by exceptions is cumbersome ("departed bookings" exception breaks this loop)
                        Debug.WriteLine(ex.Message);
                    }
                }
            }
            finally
            {
                await PopulateBookingsAsync();
            }
        }

        /// <summary>
        /// To be used for CheckIn calls or when finding a booking who's checkin period is within 24 hours.
        /// </summary>
        /// <returns>The booking async.</returns>
        /// <param name="recordLocator">Record locator.</param>
        /// <param name="lastName">Last name.</param>
        public async Task<Response<BookingEntity>> FindBookingAsync(string recordLocator, string lastName)
        {
            var result = new Response<BookingEntity>(new BookingEntity(), false);
            var findBookingRequest = new FindBookingRequest { ReservationCriteria = new ReservationCriteria { RecordLocator = recordLocator, LastName = lastName } };
            var response = await _tibcoService.FindBookingAsync(findBookingRequest);
            if (response.IsSuccess)
            {
                result.IsSuccess = response.Data.IsSuccess;
                result.Data = response.Data.ToBookingEntity();
                result.Data.BookingLastName = lastName;
                result.Data.ConversationID = response.Headers.GetConversationID();
                Debug.WriteLine("Found ConversationID: " + result.Data.ConversationID);
            }
            result.Message = response.Message + "\n" + response.Data.Message;
            return result;
        }

        public Task<Response<BookingEntity>> FindAndSaveBookingAsync(string recordLocator, string lastName)
        {
            return FindAndSaveBookingAsync(recordLocator, lastName, true);
        }

        public async Task<Response<BookingEntity>> FindAndSaveBookingAsync(string recordLocator, string lastName, bool validateCheckinEligibility)
        {
            var result = await FindBookingAsync(recordLocator, lastName);
            if (result.IsSuccess)
            {
                await _bookingRepository.AddBooking(result.Data);
                await PopulateBookingsAsync();
            }
            if (validateCheckinEligibility)
            {
                if (!result.Data.Passengers.Any(x => x.Segments.Any(y => y.CanCheckIn)))
                {
                    throw ExceptionFactory.CheckIn.NotEligible();
                }
                if (result.Data.Passengers.Any(x => x.IsInhibitedBySSR))
                {
                    throw ExceptionFactory.CheckIn.IsInhibitedBySSR();
                }
            }
            return result;
        }


        public async Task<FlightStatusItem> GetFlightStatus(DateTime departureDate, string departureCode, string flightNumber)
        {
            var response = await _opsApiService.GetFlightStatus(new FlightStatusRequest
            {
                DepartureDate = departureDate,
                DepartureCode = departureCode,
                FlightNumber = flightNumber
            });

            return response.IsSuccess ? response.Data.ToFlightStatus() : new FlightStatusItem();
        }

        private async Task<Response<BookingEntity>> RetrieveBookingAsync(string recordLocator, string lastName)
        {
            var result = new Response<BookingEntity>(new BookingEntity(), false);
            var deviceId = _appSettings.PushNotificationsToken;

            var retrieveBookingRequest = new RetrieveBookingRequest
            {
                RecordLocator = recordLocator,
                LastName = lastName,
                DeviceId = deviceId
            };

            var response = await _tibcoService.RetrieveBookingAsync(retrieveBookingRequest);
            if (response.IsSuccess)
            {
                result.Message = response.Message;
                result.IsSuccess = response.Data.IsSuccess;
                result.Data = response.Data.ToBookingEntity();
                var bookingNameItems = response.Data.Bookings.Booking.FirstOrDefault().BookingName.FirstOrDefault().BookingNameItem;
                if (bookingNameItems == null || !bookingNameItems.Any())
                {
                    throw ExceptionFactory.FindBooking.FlightNotInitialized(null, null);
                }
                result.Data.BookingLastName = lastName;
            }

            return result;
        }

        public async Task RemoveBooking(string recordLocator)
        {
            var checkinManager = Mvx.IoCProvider.Resolve<ICheckInManager>();
            checkinManager.ClearBoardingPassCache(recordLocator);
            await _bookingRepository.RemoveBookingAsync(recordLocator);
            await PopulateBookingsAsync();
        }

        public async Task PopulateBookingsAsync()
        {
            var bookings = await LoadBookingsAsync();
            await PopulateBookingItemsAsync(bookings);
            await PopulateTripItemsAsync(bookings);
        }

        public async Task PopulateBookingItemsAsync(IEnumerable<BookingEntity> bookings)
        {
            BookingItems.Clear();

            var bookingItems = bookings
                .SelectMany(x => x.PassengerSegments.Select(y => y.ToBookingItem(x))) // flatten all the bookings
                .GroupBy(x => x.BookingReference + x.FlightNumber) // group them
                .SelectMany(x => x.Take(1)) // only take the first one in each reference as their may be more than 1 passenger, only show first first passgenger
                .Where(x => x.DateFrom > DateTime.Now) // only future dated boookings
                .OrderBy(x => x.DateFrom);

            if (bookingItems != null)
            {
                BookingItems.AddRange(bookingItems);
            }

            await RaisePropertyChanged(() => BookingItems);
        }

        private async Task PopulateTripItemsAsync(IEnumerable<BookingEntity> bookings)
        {
            var tripItems = bookings.OrderBy(x => x.FirstSegment.FromTime)
                .Select(x => x.ToTripItem())
                .Where(x => x is TripItem);

            var tripItemsWithSections = AppendSections(tripItems);

            tripItemsWithSections
                .OfType<TripItem>()
                .ForEach(x => x.SummaryModeToggled = OnItemSummaryModeToggled);

            TripItems.Clear();
            if (tripItemsWithSections != null)
            {
                TripItems.AddRange(tripItemsWithSections);
            }

            await RaisePropertyChanged(() => TripItems);

            #region Local Functions

            void OnItemSummaryModeToggled(TripItem tripItem)
            {
                if (!tripItem.IsSummaryMode)
                {
                    var previouslyExpandedItems = TripItems
                        .Where(x => x != tripItem && x is TripItem trip && !trip.IsSummaryMode)
                        .Cast<TripItem>();

                    previouslyExpandedItems
                        .ForEach(x => x.IsSummaryMode = true);
                }
            }

            List<ITripItem> AppendSections(IEnumerable<TripItem> source)
            {

                var result = new List<ITripItem>();
                var futureItems = source.Where(x => x.TripEndDate >= DateTime.Now);
                AddDateSections(result, futureItems);
                var pastItems = source.Where(x => x.TripEndDate < DateTime.Now);
                if (pastItems.Any())
                {
                    result.Add(new PastTripsSectionHeaderItem());
                }
                AddDateSections(result, pastItems);
                return result;
            }

            void AddDateSections(List<ITripItem> result, IEnumerable<TripItem> futureItems)
            {
                var currentMonth = string.Empty;
                foreach (var item in futureItems)
                {
                    if (item.Month != currentMonth)
                    {
                        currentMonth = item.Month;
                        result.Add(new TripDateSectionHeaderItem(currentMonth, item.FromTime));
                    }
                    result.Add(item);
                }
            }

            #endregion
        }

        public async Task<string> RefreshConversationID(string recordLocator, string lastName)
        {
            var response = await FindBookingAsync(recordLocator, lastName);
            if (response.IsSuccess)
            {
                await _bookingRepository.AddBooking(response.Data);
                var tripItem = TripItems.OfType<TripItem>().FirstOrDefault(x => x.BookingReference == recordLocator);
                if (tripItem != null)
                {
                    tripItem.ConversationID = response.Data.ConversationID;
                }

                var bookingItem = BookingItems.FirstOrDefault(x => x.BookingReference == recordLocator);
                if (bookingItem != null)
                {
                    bookingItem.ConversationID = response.Data.ConversationID;
                }

                return response.Data.ConversationID;
            }

            return string.Empty;
        }

        #endregion //Methods
    }
}
