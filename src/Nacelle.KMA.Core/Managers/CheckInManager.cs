#region Using Directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross;
using Nacelle.Core.Models;
using Nacelle.KMA.API.Exceptions;
using Nacelle.KMA.API.Exceptions.Factories;
using Nacelle.KMA.API.Models.Requests;
using Nacelle.KMA.API.Models.Responses;
using Nacelle.KMA.API.Services;
using Nacelle.KMA.Core.Caching;
using Nacelle.KMA.Core.Mappers;
using Nacelle.KMA.Core.Models;
using Nacelle.KMA.Core.Models.Entites;
using Nacelle.KMA.Core.Models.Items;

#endregion //Using Directives

namespace Nacelle.KMA.Core.Managers
{
    public class CheckInManager : ICheckInManager
    {
        #region Constructors

        public CheckInManager(ITibcoService tibcoService, IBookingManager bookingManager, ICacheService cacheService)
        {
            _tibcoService = tibcoService;
            _bookingManager = bookingManager;
            _cacheService = cacheService;
        }

        #endregion //Constructors

        #region Fields

        private const string cacheKeyPrefixBpe = "bpe";
        private readonly ITibcoService _tibcoService;
        private readonly IBookingManager _bookingManager;
        private readonly ICacheService _cacheService;

        #endregion //Fields

        #region Methods

        public async Task UpdatePassengersAsync(Dictionary<string, string> passengerWeightCategory, ReferenceData referenceData, bool returnSession = false)
        {
            var updatePassengerReq = new UpdatePassengerRequest
            {
                ReturnSession = returnSession,
                PassengerDetails = passengerWeightCategory.Select(x => new UpdatePassengerDetails { PassengerId = x.Key, WeightCategory = x.Value, Documents = new List<Documents>() }).ToList()
            };

            var response = await SendRequestWithRetryAsync
            (
                () => _tibcoService.UpdatePassengerAsync(updatePassengerReq, referenceData.ConversationID), referenceData
            );

            if (!response.IsSuccess)
            {
                ExceptionFactory.General.WithMessage(response.Message);
            }
        }

        public async Task UpdatePassengersAsync(IEnumerable<TravellerItem> travellers, ReferenceData referenceData, bool returnSession = false)
        {
            var request = new UpdatePassengerRequest() { ReturnSession = returnSession };
            request.PassengerDetails = new List<UpdatePassengerDetails>();
            foreach (var traveller in travellers)
            {
                var updatePassengerDetails = new UpdatePassengerDetails { PassengerId = traveller.Id };
                updatePassengerDetails.WeightCategory = traveller.SelectedWeightCategoryName;
                if (traveller.RequiresPassport && traveller.PassportDetails != null)
                {
                    updatePassengerDetails.Documents = new List<Documents>
                    {
                        new Documents
                        {
                            Document = new Document
                            {
                                Id = $"{traveller.Id}.d01",
                                Type = "PASSPORT",
                                Number = traveller.PassportDetails.PassportNumber,
                                PersonName = new API.Models.Common.PersonName
                                {
                                    First = traveller.FirstName,
                                    Last = traveller.LastName,
                                    //Raw = null,
                                },
                                Nationality = traveller.PassportDetails.NationalityCode,
                                CountryOfBirth = traveller.PassportDetails.NationalityCode,
                                PlaceOfBirth = traveller.PassportDetails.NationalityCode,
                                DateOfBirth = new DateTime(traveller.PassportDetails.BirthYear, traveller.PassportDetails.BirthMonth, traveller.PassportDetails.BirthDay).ToString("yyyy-MM-dd"),
                                IssuingCountry = traveller.PassportDetails.IssuingCountryCode,
                                ExpiryDate = new DateTime(traveller.PassportDetails.ExpirationYear, traveller.PassportDetails.ExpirationMonth, traveller.PassportDetails.ExpirationDay).ToString("yyyy-MM-dd"),
                                Gender = traveller.PassportDetails.SelectedGenderName,
                            }
                        }
                    };
                }
                if (traveller.RequiresEmergencyContact && traveller.EmergencyContact != null)
                {
                    updatePassengerDetails.EmergencyContacts = new List<API.Models.Requests.EmergencyContact>
                    {
                        new API.Models.Requests.EmergencyContact
                        {
                            Id = $"{traveller.Id}.ec01",
                            Name = traveller.EmergencyContact.Name,
                            CountryCode = traveller.EmergencyContact.CountryCode,
                            ContactDetails = traveller.EmergencyContact.FormattedContactNumber()
                        }
                    };
                }
                request.PassengerDetails.Add(updatePassengerDetails);
            }

#if DEBUG
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(request);
#endif
            var response = await SendRequestWithRetryAsync
            (
                () => _tibcoService.UpdatePassengerAsync(request, referenceData.ConversationID), referenceData
            );

            if (!response.IsSuccess)
            {
                ExceptionFactory.General.WithMessage(response.Message);
            }
        }

        public async Task<SeatMapEntity> GetSeatMapAsync(string segmentId, ReferenceData referenceData, bool returnSession = false)
        {
            return await GetSeatMapAsync(new List<string> { segmentId }, referenceData, returnSession);
        }

        public async Task<SeatMapEntity> GetSeatMapAsync(IEnumerable<string> segmentIds, ReferenceData referenceData, bool returnSession = false)
        {
            var viewSeatRequest = new ViewSeatMapRequest { SegmentIds = segmentIds.ToList(), ReturnSession = returnSession };

            var response = await SendRequestWithRetryAsync
            (
                () => _tibcoService.ViewSeatMapAsync(viewSeatRequest, referenceData.ConversationID), referenceData
            );

            if (response.IsSuccess)
            {
                return response.Data.ToSeatMapEntity();
            }
            else
            {
                ExceptionFactory.General.WithMessage(response.Message);
            }

            return new SeatMapEntity();
        }

        // TODO :: return mapped object?
        public async Task SelectSeatsAsync(string seatNumber, string passengerFlightId, ReferenceData referenceData, bool returnSession = true)
        {
            var selectSeatsReq = new SelectSeatsRequest
            {
                ReturnSession = returnSession,
                SeatNumber = seatNumber,
                PassengerFlightId = passengerFlightId
            };

            var response = await SendRequestWithRetryAsync
             (
                 () => _tibcoService.SelectSeatsAsync(selectSeatsReq, referenceData.ConversationID), referenceData
             );

            if (!response.IsSuccess)
            {
                ExceptionFactory.General.WithMessage(response.Message);
            }
        }

        public async Task<IEnumerable<BoardingPassEntity>> CheckInAsync(List<string> passengerIds, List<string> passengerFlightIds, ReferenceData referenceData, bool returnSession = true)
        {
            var checkinReq = new CheckInRequest
            {
                ReturnSession = returnSession,
                PassengerIds = passengerIds,
                OutputFormat = "BPXML"
            };

            var response = await SendRequestWithRetryAsync
             (
                 () => _tibcoService.CheckInAsync(checkinReq, referenceData.ConversationID), referenceData
             );

            IEnumerable<BoardingPassEntity> boardingPassEntities = new List<BoardingPassEntity>();
            if (response.IsSuccess)
            {
                await CheckForQJumpAsync(passengerFlightIds, response.Data.BoardingPasses);
                boardingPassEntities = response.Data.ToBoardingPassEntity();
                var cacheKey = MakeBoardingPassEntitiesCacheKey(referenceData.BookingReference, passengerIds);

                _cacheService.SetValue<IEnumerable<BoardingPassEntity>>(cacheKey, boardingPassEntities, 5);
            }
            else
            {
                ExceptionFactory.General.WithMessage(response.Message);
            }

            return boardingPassEntities;
        }

        public async Task<IEnumerable<BoardingPassEntity>> GetBoardingPassesAsync(IEnumerable<string> passengerFlightIds, ReferenceData referenceData, bool returnSession = false)
        {
            async Task<IEnumerable<BoardingPassEntity>> fetchFunc()
            {
                if (passengerFlightIds == null && passengerFlightIds.Any())
                {
                    throw ExceptionFactory.BoardingPass.NoCheckedInPassengers();
                }
                var boardingPassReq = new BoardingPassRequest
                {
                    ReturnSession = returnSession,
                    PassengerFlightIds = passengerFlightIds.ToList(),
                    OutputFormat = "BPXML"
                };
                var retryCount = 0;
                var response = await SendRequestWithRetryAsync
                (
                    () => _tibcoService.BoardingPassAsync(boardingPassReq, referenceData.ConversationID), referenceData
                );
                if (response.IsSuccess)
                {
                    await CheckForQJumpAsync(passengerFlightIds, response.Data.BoardingPasses);
                    return response.Data.ToBoardingPassEntity();
                }
                return null;
            }

            var cacheKey = MakeBoardingPassEntitiesCacheKey(referenceData.BookingReference, passengerFlightIds);

            var boardingPassEntities = await _cacheService.GetOrUpdateValue<IEnumerable<BoardingPassEntity>>(
                key: cacheKey,
                fetchFunc: fetchFunc,
                expiryDays: 5,
                forceRefresh: true); // Always try get a fresh copy of the boarding pass, fall back on cache if it fails

            if (boardingPassEntities == null)
            {
                boardingPassEntities = _cacheService.GetValue<IEnumerable<BoardingPassEntity>>(cacheKey);

                if (boardingPassEntities == null)
                {
                    throw ExceptionFactory.General.SomethingWentWrong();
                }
            }


            return boardingPassEntities;
        }

        private async Task<Response<T>> SendRequestWithRetryAsync<T>(Func<Task<Response<T>>> p, ReferenceData referenceData) where T : new()
        {
            var retryCount = 0;
            var response = new Response<T>(new T(), false);
            do
            {
                try
                {
                    Debug.WriteLine($"SendRequestWithRetryAsync Attempt#${retryCount + 1}");
                    response = await p.Invoke();
                    if (response.IsSuccess)
                    {
                        break;
                    }
                }
                catch (ExpiredSession)
                {
                    Debug.WriteLine($"Has expired conversation ID");
                    referenceData.ConversationID = await _bookingManager.RefreshConversationID(referenceData.BookingReference, referenceData.LastName);
                    Debug.WriteLine($"Got New ConvrestationID: " + referenceData?.ConversationID);
                    retryCount++;
                }

            } while (retryCount < 2);

            return response;
        }

        private async Task CheckForQJumpAsync(IEnumerable<string> passengerFlightIds, IEnumerable<BoardingPassElement> boardingPassElements)
        {
            if (boardingPassElements == null)
                return;

            foreach (var item in boardingPassElements)
            {
                if (string.IsNullOrWhiteSpace(item.BoardingPass.RecordLocator))
                    continue;

                var booking = await _bookingManager.LoadBookingAsync(item.BoardingPass.RecordLocator);
                // FindBooking is the only source of the QJump information, need to cross-check with this info, just as the Cordova App does it.
                var flight = booking.PassengerSegments.FirstOrDefault(x => x.PassengerFlightId == passengerFlightIds.FirstOrDefault());
                item.HasQJump = flight.HasQJump;
            }
        }

        public Task<byte[]> FormattedForWalletBoardingPassesAsync(string boardingPassJson)
        {
            var service = Mvx.IoCProvider.Resolve<INotificationsApiService>();

            return service.BoardingPassWalletBytesAsync(boardingPassJson);
        }

        private string MakeBoardingPassEntitiesCacheKey(string bookingRef, IEnumerable<string> passengerFlightIds)
        {
            var passengerIds = passengerFlightIds.OrderBy(x => x);
            return $"{cacheKeyPrefixBpe}_{bookingRef}_{string.Join("_", passengerIds)}";
        }

        public void ClearBoardingPassCache(string bookingRef)
        {
            _cacheService.ClearWhereKeyPrefix($"{cacheKeyPrefixBpe}_{bookingRef}");
        }

        #endregion //Methods
    }
}
