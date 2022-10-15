#region Using Directives

using System.Collections.Generic;
using System.Linq;
using Nacelle.KMA.API.Models.Enums;
using Nacelle.KMA.API.Models.Responses;
using Nacelle.KMA.Core.ExtensionMethods;
using Nacelle.KMA.Core.Models.Entites;
using Nacelle.KMA.Core.Models.Enums;
using Nacelle.KMA.Core.Models.Items;

#endregion //Using Directives

namespace Nacelle.KMA.Core.Mappers
{
    public static class DomainMappers
    {
        #region Methods

        public static TripItem ToTripItem(this Reservation reservation)
        {
            var segment = reservation.Itinerary.ItineraryPart[0].Segment[0];
            var airport = segment.FlightDetail[0].ArrivalAirport.ToAirPortDescription();
            var result = new TripItem
            {
                Caption = "Trip to " + airport,
                Day = segment.FlightDetail[0].DepartureTime.Day.ToString(),
                Month = segment.FlightDetail[0].DepartureTime.ToString("MMM"),
                BookingReference = reservation.RecordLocator
            };
            var flightItems = new List<BookingItem>();
            var flightDetails = reservation.Itinerary.ItineraryPart
                .SelectMany(x => x.Segment)
                .SelectMany(x => x.FlightDetail);
            foreach (var flightDetail in flightDetails)
            {
                flightItems.Add(flightDetail.ToFlightItem());
            }
            result.FlightItems = flightItems.ToList();
            return result;
        }

        #region Bookings

        public static TripItem ToTripItem(this BookingEntity bookingEntity)
        {
            if (bookingEntity.Passengers?.FirstOrDefault()?.Segments?.FirstOrDefault() is SegmentEntity segment)
            {
                var airport = segment.ToAirport.ToAirPortDescription();
                var result = new TripItem
                {
                    Caption = "Trip to " + airport,
                    Day = segment.FromTime.Day.ToString(),
                    Month = segment.FromTime.ToString("MMM"),
                    BookingReference = bookingEntity.RecordLocator,
                    FromTime = segment.FromTime,
                    ConversationID = bookingEntity.ConversationID,
                };

                foreach (var segmentEntity in bookingEntity.Passengers.First().Segments)
                {
                    result.FlightItems.Add(segmentEntity.ToBookingItem(bookingEntity.ConversationID));
                }
                return result;
            }

            return null;
        }

        public static IEnumerable<CheckInItem> ToCheckInItems(this BookingEntity bookingEntity, BookingItem bookingItem = null)
        {
            return ToCheckInItems(bookingEntity, null, bookingItem);
        }

        public static IEnumerable<CheckInItem> ToCheckInItems(this BookingEntity bookingEntity, string segmentId, BookingItem bookingItem = null)
        {
            return bookingEntity.Passengers.ToCheckInItems(segmentId, bookingItem);
        }

        public static IEnumerable<CheckInItem> ToCheckInItemsEligible(this BookingEntity bookingEntity)
        {
            var passengersWithAnyEligibleSegments = bookingEntity.Passengers.Where(x => x.Segments.Any(y => y.CanCheckIn));

            foreach (var passenger in passengersWithAnyEligibleSegments)
            {
                passenger.Segments.RemoveAll(x => !x.CanCheckIn);
            }

            return passengersWithAnyEligibleSegments.ToCheckInItems();
        }

        private static IEnumerable<CheckInItem> ToCheckInItems(this IEnumerable<PassengerEntity> passengerEntities, BookingItem bookingItem = null)
        {
            return ToCheckInItems(passengerEntities, null, bookingItem);
        }

        private static IEnumerable<CheckInItem> ToCheckInItems(this IEnumerable<PassengerEntity> passengerEntities, string segmentId, BookingItem bookingItem = null)
        {
            List<CheckInItem> checkInItems = new List<CheckInItem>();
            List<PassengerEntity> passengerEntityList = passengerEntities.ToList();
            for (int i = 0; i < passengerEntityList.Count(); i++)
            {
                CheckInItem checkInItem = new CheckInItem() { TravellerItems = new List<TravellerItem>() };
                PassengerEntity passengerEntity = passengerEntityList[i];
                for (int j = 0; j < passengerEntity.Segments.Count; j++)
                {
                    SegmentEntity segmentEntity = passengerEntity.Segments[j];
                    if (!string.IsNullOrEmpty(segmentId) && !segmentId.ToLower().Equals(segmentEntity.Id))
                    {
                        continue; //Need to filter on segment and it's not for this segment.
                    }
                    TravellerItem travellerItem = new TravellerItem()
                    {
                        Id = passengerEntity.Id,
                        Index = i + 1,
                        FirstName = passengerEntity.FirstName,
                        LastName = passengerEntity.LastName,
                        RequiresPassport = passengerEntity.RequiresPassport,
                        RequiresEmergencyContact = passengerEntity.RequiresEmergencyContact,
                        SelectedWeightCategoryIndex = (int)passengerEntity.WeightCategory.ToWeightCategory(),
                        HasUpdatedWeightCategory = passengerEntity.WeightCategory.ToWeightCategory() != WeightCategory.Unknown,
                        PassengerFlightId = segmentEntity.PassengerFlightId,
                        SegmentId = segmentEntity.Id,
                        SeatItem = new SeatItem(segmentEntity.Seat),
                        HasInfant = passengerEntity.HasInfant,
                        InfantName = passengerEntity.InfantName?.ToTitleCase(),
                        InfantId = passengerEntity.InfantId,
                        PassengerType = passengerEntity.PassengerType
                    };
                    checkInItem.TravellerItems.Add(travellerItem);
                }
                checkInItem.BookingItems = bookingItem == null ?
                    passengerEntity.Segments.Select(y => y.ToBookingItem(null)).ToList() :
                    new List<BookingItem>() { bookingItem };
                checkInItems.Add(checkInItem);
            }
            return checkInItems;
        }

        public static BookingItem ToBookingItem(this SegmentEntity segmentEntity, string conversationId)
        {
            return segmentEntity.ToBookingItem(null, conversationId);
        }

        public static BookingItem ToBookingItem(this SegmentEntity segmentEntity, BookingEntity bookingEntity, string conversationId = null)
        {
            return new BookingItem
            {
                Carrier = segmentEntity.Carrier,
                Number = segmentEntity.Carrier + segmentEntity.FlightNumber,
                From = segmentEntity.FromAirport,
                To = segmentEntity.ToAirport,
                DateFrom = segmentEntity.FromTime,
                DateTo = segmentEntity.ToTime,
                BookingReference = bookingEntity?.RecordLocator ?? segmentEntity.RecordLocator,
                CanCheckIn = segmentEntity.CanCheckIn,
                IsViewBoardingPassVisible = segmentEntity.HasCheckedIn,
                HasCheckedIn = segmentEntity.HasCheckedIn,
                BookingLastName = segmentEntity.LastName,
                ConversationID = bookingEntity == null ? conversationId : bookingEntity.ConversationID,
                Gate = segmentEntity.Gate,
                Terminal = segmentEntity.Terminal,
                FullName = $"{segmentEntity.FirstName}  {segmentEntity.LastName}",
                HasQJump = segmentEntity.HasQJump,
                Seat = segmentEntity.Seat,
                FlightNumber = segmentEntity.FlightNumber,
                PassengerFlightId = segmentEntity.PassengerFlightId,
                SegmentId = segmentEntity.Id,
            };
        }

        public static BookingItem ToFlightItem(this FlightDetail flightDetail)
        {
            return new BookingItem
            {
                Number = flightDetail.Airline + flightDetail.FlightNumber,
                From = flightDetail.DepartureAirport,
                To = flightDetail.ArrivalAirport,
                DateFrom = flightDetail.DepartureTime.LocalDateTime,
                DateTo = flightDetail.ArrivalTime.LocalDateTime,
                FlightNumber = flightDetail.FlightNumber
            };
        }

        public static BookingEntity ToBookingEntity(this FindBookingResponse response)
        {
            var result = new BookingEntity();
            if (!response.IsSuccess)
            {
                return result;
            }
            result.RecordLocator = response.Reservation.RecordLocator;
            foreach (var passenger in response.Reservation.Passengers.Passenger)
            {
                var passengerEntity = new PassengerEntity
                {
                    Id = passenger.Id,
                    FirstName = passenger.PersonName.First,
                    LastName = passenger.PersonName.Last,
                    PassengerType = passenger.Type.Value.PassengerTypeFromString(),
                    WeightCategory = passenger.WeightCategory
                };
                if (passenger.Eligibilities != null)
                {
                    passengerEntity.RequiresPassport = passenger.Eligibilities.Eligibility.Any(x => x.NotEligible && x.Reason.Any(y => y.Category.Equals("PASSENGER_DOES_NOT_HAVE_REQUIRED_IDENTITY_DOCUMENTS")));
                    passengerEntity.RequiresEmergencyContact = passenger.Eligibilities.Eligibility.Any(x => x.NotEligible && x.Reason.Any(y => y.Category.Equals("PASSENGER_DOES_NOT_HAVE_REQUIRED_EMERGENCY_CONTACT")));
                    passengerEntity.IsInhibitedBySSR = passenger.Eligibilities.Eligibility.Any(x => x.NotEligible && x.Type.Equals("CHECK_IN") && x.Reason.Any(y => y.Category.Equals("INHIBITED_SSR_FOUND")));
                }
                if (response.Reservation.Itinerary.ItineraryPart.Any())
                {
                    //      var flightDetails = reservation.Itinerary.ItineraryPart
                    //.SelectMany(x => x.Segment)
                    //.SelectMany(x => x.FlightDetail);
                    foreach (var segment in response.Reservation.Itinerary.ItineraryPart.First().Segment)
                    {
                        var segmentEntity = new SegmentEntity { Id = segment.Id };
                        var flightDetail = segment.FlightDetail.First();
                        segmentEntity.FromAirport = flightDetail.DepartureAirport;
                        segmentEntity.ToAirport = flightDetail.ArrivalAirport;
                        segmentEntity.FromTime = flightDetail.DepartureTime.LocalDateTime;
                        segmentEntity.ToTime = flightDetail.ArrivalTime.LocalDateTime;
                        segmentEntity.Carrier = flightDetail.OperatingAirline;
                        segmentEntity.FlightNumber = flightDetail.OperatingFlightNumber;
                        segmentEntity.RecordLocator = result.RecordLocator;
                        segmentEntity.LastName = passenger.PersonName.Last;
                        segmentEntity.Gate = flightDetail.DepartureGate;
                        segmentEntity.Terminal = flightDetail.DepartureTerminal;

                        // can check-in by default
                        segmentEntity.IsEligibleForCheckIn = true;
                        if (segment.Eligibilities != null)
                        {
                            segmentEntity.IsEligibleForCheckIn = segment.Eligibilities.Eligibility.Any(e => !e.NotEligible);
                        }
                        var segmentId = passenger.Id + "." + segment.Id;
                        //segmentEntity.PassengerFlightId = flightDetail.Id;
                        try
                        {
                            var passengerSegment = passenger.PassengerSegments.PassengerSegment.FirstOrDefault(x => x.Id == segmentId);
                            var passengerFlight = passengerSegment.PassengerFlights.FirstOrDefault();
                            if (passengerFlight != null)
                            {
                                segmentEntity.HasCheckedIn = passengerFlight.CheckedIn;
                                segmentEntity.Seat = passengerFlight.Seat?.Value;
                                segmentEntity.FirstName = passengerEntity.FirstName;
                                segmentEntity.LastName = passengerEntity.LastName;
                                segmentEntity.PassengerFlightId = passengerFlight.Id;
                            }

                            if (passengerSegment.AirExtra != null)
                            {
                                segmentEntity.HasQJump = passengerSegment.AirExtra.Any(x => x.Type == "ANCILLARY" && x.Ancillary.CommercialName == "Q JUMP");
                            }

                            segmentEntity.TicketIssued = passenger.Ticket.FirstOrDefault().Issued;
                        }
                        catch
                        {
                            // TODO: Rather put a null check than do a try/catch
                        }
                        passengerEntity.Segments.Add(segmentEntity);
                    }
                }
                result.Passengers.Add(passengerEntity);
            }

            // Assign each infant to the respective adult
            var adults = result.Passengers.Where(x => x.PassengerType == PassengerTypes.Adult).ToArray();
            var infants = result.Passengers.Where(x => x.PassengerType == PassengerTypes.Infant).ToArray();
            for (var i = 0; i < infants.Count(); i++)
            {
                if (i < adults.Count())
                {
                    adults[i].HasInfant = true;
                    adults[i].InfantName = $"{infants[i].FirstName} {infants[i].LastName}";
                    adults[i].InfantId = infants[i].Id;
                }
            }

            return result;
        }

        public static BookingEntity ToBookingEntity(this RetrieveBookingIXResponse response)
        {
            var result = new BookingEntity();
            if (!response.IsSuccess || response.Bookings.Count == 0)
            {
                return result;
            }

            result.RecordLocator = response.Bookings.Booking.First().Rloc;

            foreach (var bookingName in response.Bookings.Booking.First().BookingName)
            {
                var passengerEntity = new PassengerEntity
                {
                    FirstName = bookingName.FirstName,
                    LastName = bookingName.LastName,
                    PassengerType = (PassengerTypes)bookingName.PassengerType,
                };

                if (bookingName?.BookingNameItem == null)
                {
                    return result;
                }

                if (bookingName?.BookingNameItem is IEnumerable<BookingNameItem> bookingNameItems)
                {
                    foreach (var bookingItem in bookingNameItems.OrderBy(x => x.DepartureDate))
                    {
                        var segmentEntity = new SegmentEntity
                        {
                            FromAirport = bookingItem.Origin,
                            ToAirport = bookingItem.Destination,
                            FromTime = bookingItem.DepartureDate.LocalDateTime,
                            ToTime = bookingItem.ArrivalDateTime.LocalDateTime,
                            Carrier = bookingItem.OperatingCarrier,
                            FlightNumber = bookingItem.OperatingFlightNumber,
                            RecordLocator = result.RecordLocator,
                            FirstName = passengerEntity.FirstName,
                            LastName = passengerEntity.LastName,
                            HasFlightInTheFuture = bookingItem.HasFlightInTheFuture
                        };

                        passengerEntity.Segments.Add(segmentEntity);
                        if (segmentEntity.HasFlightInTheFuture)
                        {
                            result.HasFlightInTheFuture = segmentEntity.HasFlightInTheFuture;
                        }
                    }
                }

                result.Passengers.Add(passengerEntity);
            }

            return result;
        }

        #endregion //Bookings

        #region Seat Map

        public static CabinEntity ToCabinEntity(this Cabin cabin)
        {
            return new CabinEntity
            {
                BookingClass = cabin.BookingClass,
                Columns = cabin.Columns.Select(x => new ColumnEntity
                {
                    Id = x.Id,
                    Name = x.Name,
                    Characteristics = x.Characteristics?
                    .Select(z => new CharacteristicEntity { Location = z.Location, Value = z.Value }).ToList()
                }).ToList(),
                Rows = cabin.Rows.Select(x => new RowEntity
                {
                    Number = x.Number,
                    Characteristics = x.Characteristics,
                    Slots = x.Slots
                                    .Where(y => y.Seat != null)
                                    .Select(y => new SlotEntity
                                    {
                                        ColumnRef = y.ColumnRef,
                                        Seat = new SeatEntity
                                        {
                                            Number = y.Seat.Number,
                                            Status = y.Seat.Status.ToSeatStatus(),
                                            Limitations = y.Seat.Limitations
                                        }
                                    }).ToList()
                }).ToList()
            };
        }

        public static SeatMapEntity ToSeatMapEntity(this ViewSeatMapResponse response)
        {
            var cabin = response.SeatMap.FirstOrDefault()?.Cabins.FirstOrDefault()?.ToCabinEntity();

            return new SeatMapEntity { Cabin = cabin };
        }

        public static IEnumerable<RowItem> ToRowItems(this SeatMapEntity seatMapEntity, bool hasInfantOrIsChild)
        {
            SeatStatus ComputeSeatStatus(SeatEntity seatEntity)
            {
                return IsNextToExitDoor(seatEntity) && hasInfantOrIsChild
                    ? SeatStatus.Unavailable
                    : seatEntity.Status;
            }

            bool IsNextToExitDoor(SeatEntity seatEntity)
            {
                return
                    seatEntity.Limitations != null &&
                    seatEntity.Limitations.Any(q => q.Contains("NEXT_TO_EXIT_DOOR"));
            }

            var columns = seatMapEntity.Cabin.Columns.ToDictionary(x => x.Id, x => x.Name);

            var rowItems = seatMapEntity.Cabin.Rows.Select(x => new RowItem
            {
                Row = x.Number,
                IsExitRow = x.Characteristics != null && x.Characteristics.Any(q => q.Equals("EXIT")),
                SeatItems = x.Slots.Select(y => new SeatItem
                {
                    Row = x.Number,
                    Column = int.Parse(y.ColumnRef[1].ToString()),
                    ColumnLetter = columns[y.ColumnRef],
                    IsExit = IsNextToExitDoor(y.Seat),
                    SeatStatus = ComputeSeatStatus(y.Seat)
                }).ToList()

            });

            return rowItems;
        }

        //public static IEnumerable<SeatItem> ToSeatItems(this SeatMapEntity seatMapEntity)
        //{
        //    var columns = seatMapEntity.Cabin.Columns.ToDictionary(x => x.Id, x => x.Name);

        //    var seatItems = seatMapEntity.Cabin.Rows.SelectMany(x => x.Slots.Select(y => new SeatItem
        //    {
        //        Row = x.Number,
        //        Column = y.ColumnRef[1],
        //        ColumnLetter = columns[y.ColumnRef],
        //        SeatStatus = y.Seat.Status
        //    }));

        //    return seatItems;
        //}

        public static SeatStatus ToSeatStatus(this string status)
        {
            switch (status.ToLowerInvariant())
            {
                case "available":
                    return SeatStatus.Available;
                case "blocked":
                    return SeatStatus.Unavailable;
                case "occupied":
                    return SeatStatus.Unavailable;
                default:
                    return SeatStatus.Available;
            }
        }

        public static WeightCategory ToWeightCategory(this string weightCategory)
        {
            if (string.IsNullOrWhiteSpace(weightCategory))
            {
                return WeightCategory.Unknown;
            }

            switch (weightCategory.ToLowerInvariant())
            {
                case "adult_male":
                    return WeightCategory.AdultMale;
                case "adult_female":
                    return WeightCategory.AdultFemale;
                case "child":
                    return WeightCategory.Child;
                default:
                    return WeightCategory.Unknown;
            }
        }

        #endregion //Seat Map

        #region Boarding Pass

        public static IEnumerable<BoardingPassEntity> ToBoardingPassEntity(this CheckInResponse response)
        {
            var boardingPassEntities = response.BoardingPasses.Select(x => x.ToBoardingPassEntity()).ToArray();
            for (var i = 0; i < boardingPassEntities.Length; i++)
            {
                var bpe = boardingPassEntities[i];
                {
                    if (string.IsNullOrWhiteSpace(bpe.PassengerName))
                    {
                        var passenger = response.Reservation.Passengers.Passenger.FirstOrDefault(x => bpe.PassengerFlightId.StartsWith(x.Id, System.StringComparison.InvariantCultureIgnoreCase));
                        if (passenger != null)
                        {
                            bpe.PassengerName = $"{passenger.PersonName.First} {passenger.PersonName.Last}";
                        }
                    }
                }
            }
            return boardingPassEntities;
        }

        public static IEnumerable<BoardingPassEntity> ToBoardingPassEntity(this BoardingPassResponse response)
        {
            var boardingPassEntities = response.BoardingPasses.Select(x => x.ToBoardingPassEntity());

            return boardingPassEntities;
        }

        private static BoardingPassEntity ToBoardingPassEntity(this BoardingPassElement bpe)
        {
            return new BoardingPassEntity
            {
                PassengerFlightId = bpe.PassengerFlightId,
                PassengerName = bpe.BoardingPass.PersonName?.ToString(),
                Seat = bpe.BoardingPass.Seat.Value,
                Terminal = bpe.BoardingPass.FlightDetail.DepartureTerminal,
                Gate = bpe.BoardingPass.FlightDetail.DepartureGate,

                Zone = bpe.HasQJump ? "Q JUMP" : string.Empty,

                FlightNumber = $"{bpe.BoardingPass.FlightDetail.OperatingAirline}{bpe.BoardingPass.FlightDetail.OperatingFlightNumber}",
                DepartureDateTime = bpe.BoardingPass.FlightDetail.DepartureTime.LocalDateTime,
                ArrivalDateTime = bpe.BoardingPass.FlightDetail.ArrivalTime.LocalDateTime,

                DepartureAirport = bpe.BoardingPass.DisplayData.DepartureAirportName,
                ArrivalAirport = bpe.BoardingPass.DisplayData.ArrivalAirportName,
                DepartureAirportCode = bpe.BoardingPass.FlightDetail.DepartureAirport,
                ArrivalAirportCode = bpe.BoardingPass.FlightDetail.ArrivalAirport,

                QrCode = bpe.BoardingPass.BarCode,
                BoardingTime = bpe.BoardingPass.FlightDetail.BoardingTime.LocalDateTime,

                BoardingPassJson = Newtonsoft.Json.JsonConvert.SerializeObject(new { boardingPasses = new[] { new { boardingPass = bpe.BoardingPass } }.ToList() })
            };
        }

        public static BoardingPassItem ToBoardingPassItem(this BoardingPassEntity boardingPassEntity)
        {
            var boardingPassItem = new BoardingPassItem
            {
                PassengerName = boardingPassEntity.PassengerName?.ToTitleCase() ?? string.Empty,
                Seat = boardingPassEntity.Seat ?? string.Empty,
                Terminal = boardingPassEntity.Terminal ?? string.Empty,
                Gate = boardingPassEntity.Gate ?? string.Empty,
                Zone = boardingPassEntity.Zone ?? string.Empty,

                FlightNumber = boardingPassEntity.FlightNumber ?? string.Empty,
                DepartureDateTime = boardingPassEntity.DepartureDateTime,
                //DepartureTime = boardingPass.DepartureTime,
                ArrivalDateTime = boardingPassEntity.ArrivalDateTime,

                DepartureAirport = boardingPassEntity.DepartureAirport ?? string.Empty,
                ArrivalAirport = boardingPassEntity.ArrivalAirport ?? string.Empty,
                DepartureAirportCode = boardingPassEntity.DepartureAirportCode ?? string.Empty,
                ArrivalAirportCode = boardingPassEntity.ArrivalAirportCode ?? string.Empty,

                QrCode = boardingPassEntity.QrCode ?? string.Empty,
                BoardingTime = boardingPassEntity.BoardingTime,

                BoardingPassJson = boardingPassEntity.BoardingPassJson
            };

            return boardingPassItem;
        }

        #endregion //Boarding Pass

        #region Weather

        public static List<WeatherEntity> ToWeatherEntity(this WeatherResponse response)
        {
            if (string.IsNullOrEmpty(response?.Error) && response.Days != null)
            {
                return response.Days.Select(x => new WeatherEntity
                {
                    AirportCode = response?.AirportCode,
                    WeatherStation = response?.WeatherStation,
                    Longitude = response?.Longitude,
                    Latitude = response?.Latitude,
                    DateTime = x.DateTime,
                    CloudPercentage = x.CloudPercentage,
                    WindSpeed = x.WindSpeed,
                    WindDirection = x.WindDirection,
                    Temperature = x.Temperature,
                    MinTemperature = x.MinTemperature,
                    MaxTemperature = x.MaxTemperature,
                    RainVolume = x.RainVolume,
                    Pressure = x.Pressure,
                    SeaLevelPressure = x.SeaLevelPressure,
                    GroundLevelPressure = x.GroundLevelPressure,
                    HumidityPercentage = x.HumidityPercentage,
                    Main = x.Main,
                    Description = x.Description,
                    Icon = x.Icon
                }).ToList();
            }


            return new List<WeatherEntity>();
        }

        public static List<AirportEntity> ToAirportEntity(this AirportResponse response)
        {
            return response.Airports.Select(x => new AirportEntity
            {
                Code = x.Code,
                CountrCode = x.CountrCode,
                CountryName = x.CountryName,
                Name = x.Name
            }).ToList();
        }

        public static FlightStatusItem ToFlightStatus(this FlightStatusResponse response)
        {
            return new FlightStatusItem
            {
                Status = response?.FlightStatus
            };
        }

        #endregion //Weather

        #endregion //Methods
    }
}
