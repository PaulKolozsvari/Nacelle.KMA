#region Using Directives

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nacelle.Core.Models;
using Nacelle.Core.Services;
using Nacelle.KMA.API.Configs;
using Nacelle.KMA.API.Models.Requests;
using Nacelle.KMA.API.Models.Responses;
using System.Linq;
using Nacelle.KMA.API.Exceptions;
using Nacelle.KMA.API.Exceptions.Extensions;
using Nacelle.KMA.API.Exceptions.Factories;

#endregion //Using Directives

namespace Nacelle.KMA.API.Services
{
    public class TibcoService : Service, ITibcoService
    {
        #region Constructors

        public TibcoService(IHttpClientService clientService, TibcoConfig config) : base(clientService, config)
        {
        }

        #endregion //Constructors

        #region Methods

        public async Task<Response<FindBookingResponse>> FindBookingAsync(FindBookingRequest request)
        {
            var response = await HttpClientService.PostRequestAsync<FindBookingRequest, FindBookingResponse>(request, QueryParams, Config.BaseUrl, "findbooking");

            if (response.Data.Results != null && response.Data.Results.MustThrowExceptionIfAny())
            {
                throw response.Data.Results.ToException();
            }
            return response;
        }

        public async Task<Response<RetrieveBookingIXResponse>> RetrieveBookingAsync(RetrieveBookingRequest request)
        {
            var response = await HttpClientService.PostRequestAsync<RetrieveBookingRequest, RetrieveBookingIXResponse>(request, QueryParams, Config.BaseUrl, "retrievebookingix");

            if (response.Data.Bookings?.Count == 0)
            {
                throw ExceptionFactory.FindBooking.NotFound();
            }
            return response;
        }

        public async Task<Response<UpdatePassengerResponse>> UpdatePassengerAsync(UpdatePassengerRequest request, string conversationId)
        {
            var response = await HttpClientService.PostRequestAsync<UpdatePassengerRequest, UpdatePassengerResponse>(request, QueryParams, Config.BaseUrl, "updatepassenger", GetHeaders(conversationId));
            if (response.Data.Results != null && response.Data.Results.MustThrowExceptionIfAny())
            {
                throw response.Data.Results.ToException();
            }
            return response;
        }

        public async Task<Response<ViewSeatMapResponse>> ViewSeatMapAsync(ViewSeatMapRequest request, string conversationId)
        {
            var response = await HttpClientService.PostRequestAsync<ViewSeatMapRequest, ViewSeatMapResponse>(request, QueryParams, Config.BaseUrl, "viewseatmap", GetHeaders(conversationId));

            if (response.Data.Results != null && response.Data.Results.MustThrowExceptionIfAny())
            {
                throw response.Data.Results.ToException();
            }
            return response;
        }

        public async Task<Response<SelectSeatsResponse>> SelectSeatsAsync(SelectSeatsRequest request, string conversationId)
        {
            var response = await HttpClientService.PostRequestAsync<SelectSeatsRequest, SelectSeatsResponse>(request, QueryParams, Config.BaseUrl, "selectseats", GetHeaders(conversationId));
            if (response.Data.Results != null && response.Data.Results.MustThrowExceptionIfAny())
            {
                throw response.Data.Results.ToException();
            }
            return response;
        }

        public async Task<Response<CheckInResponse>> CheckInAsync(CheckInRequest request, string conversationId)
        {
            var response = await HttpClientService.PostRequestAsync<CheckInRequest, CheckInResponse>(request, QueryParams, Config.BaseUrl, "checkin", GetHeaders(conversationId));

            if (response.Data.Results != null && response.Data.Results.MustThrowExceptionIfAny())
            {
                throw response.Data.Results.ToException();
            }
            return response;
        }

        public async Task<Response<BoardingPassResponse>> BoardingPassAsync(BoardingPassRequest request, string conversationId)
        {
            var response = await HttpClientService.PostRequestAsync<BoardingPassRequest, BoardingPassResponse>(request, QueryParams, Config.BaseUrl, "boardingpass", GetHeaders(conversationId));
            if (response.Data.Results.MustThrowExceptionIfEmpty())
            {
                throw response.Data.Results.ToException();
            }
            return response;
        }

        public IDictionary<string, string> GetHeaders(string conversationId) => new Dictionary<string, string>() { { Constants.Headers.ConversationID, conversationId } };

        #endregion //Methods
    }
}
