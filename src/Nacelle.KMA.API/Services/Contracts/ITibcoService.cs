using System;
using System.Threading.Tasks;
using Nacelle.Core.Models;
using Nacelle.KMA.API.Models.Requests;
using Nacelle.KMA.API.Models.Responses;

namespace Nacelle.KMA.API.Services
{
    public interface ITibcoService
    {
        Task<Response<FindBookingResponse>> FindBookingAsync(FindBookingRequest request);
        Task<Response<RetrieveBookingIXResponse>> RetrieveBookingAsync(RetrieveBookingRequest request);
        Task<Response<UpdatePassengerResponse>> UpdatePassengerAsync(UpdatePassengerRequest request, string conversationId);
        Task<Response<ViewSeatMapResponse>> ViewSeatMapAsync(ViewSeatMapRequest request, string conversationId);
        Task<Response<SelectSeatsResponse>> SelectSeatsAsync(SelectSeatsRequest request, string conversationId);
        Task<Response<CheckInResponse>> CheckInAsync(CheckInRequest request, string conversationId);
        Task<Response<BoardingPassResponse>> BoardingPassAsync(BoardingPassRequest request, string conversationId);
    }
}
