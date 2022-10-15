﻿#region Using Directives

using System;
using Nacelle.KMA.Core.Builders;


#endregion //Using Directives
namespace Nacelle.KMA.Core.Models.Items
        #region Constructors

        public BookingItem()

        #endregion //Constructors

        #region Fields

        private IBookingManager _bookingManager;

        #endregion //Fields

        #region Properties

        public string Carrier { get; set; }

        // TODO: Should be using API to checkin-in is available and not rely on a client side time calc.
        public bool IsCheckInVisible => CanCheckIn && !IsViewBoardingPassVisible;
        public bool IsKululaFlight => Carrier.Equals("MN", StringComparison.OrdinalIgnoreCase);

        public string FullName { get; internal set; }

        public IConnectivityManager ConnectivityManager

        private IBookingManager BookingManager
        {
            get
            {
        }

        #endregion //Properties

        #region Methods

        protected Dictionary<string, string> InitializeAnalyticsContext()

        #endregion //Methods

        #region Commands

        public virtual ICommand FlightDetailCommand => new TrackableAsyncCommand(Constants.Analytics.Events.CardTap, DoFlightDetailCommandAsync, Constants.Analytics.Target.FlightCard, InitializeAnalyticsContext);

        #endregion //Commands

        #region Command Handlers

        protected virtual Task DoFlightDetailCommandAsync()

        private async Task DoCheckInCommandAsync()
                if (response.IsSuccess)
                {
                    this.ConversationID = response.Data.ConversationID;
                    var checkinItems = response.Data.ToCheckInItemsEligible();
                    if (checkinItems != null && checkinItems.Any())
                    {
                        await NavigationService.Navigate<CheckInfoViewModel, CheckInNavBundle>(new CheckInNavBundle
                        {
                            BookingReference = this.BookingReference,
                            ConversationID = this.ConversationID,
                            LastName = this.BookingLastName,
                            CheckInItems = checkinItems.ToList()
                        });
                    }
                    else
                    {
                        throw ExceptionFactory.CheckIn.NotEligible();
                    }
                }
            }
            if (ConnectivityManager.NetworkAccess == Nacelle.KMA.Core.Enums.NetworkAccess.None)
            ProgressActivityService.Show();
                string segment = this.SegmentId;
                var response = await BookingManager.FindBookingAsync(BookingReference, BookingLastName);
                if (response != null && response.IsSuccess) //Boarding pass code.
                {
                    this.ConversationID = response.Data.ConversationID;
                    var checkinItems = response.Data.ToCheckInItems(SegmentId);
                    await NavigationService.Navigate<BoardingPassViewModel, CheckedInNavBundle>(new CheckedInNavBundle
                    {
                        BookingReference = BookingReference,
                        LastName = BookingLastName,
                        ConversationID = this.ConversationID,
                        CheckInItems = checkinItems.ToList()
                    });
                }
            }

        #endregion //Command Handlers