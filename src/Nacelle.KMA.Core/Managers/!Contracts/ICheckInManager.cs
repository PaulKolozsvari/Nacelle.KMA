using System.Collections.Generic;
using System.Threading.Tasks;
using Nacelle.KMA.Core.Models;
using Nacelle.KMA.Core.Models.Entites;
using Nacelle.KMA.Core.Models.Items;

namespace Nacelle.KMA.Core.Managers
{
    public interface ICheckInManager
    {
        Task UpdatePassengersAsync(Dictionary<string, string> passengerWeightCategory, ReferenceData referenceData, bool returnSession = false);
        Task UpdatePassengersAsync(IEnumerable<TravellerItem> travellers, ReferenceData referenceData, bool returnSession = false);
        Task<SeatMapEntity> GetSeatMapAsync(string segmentId, ReferenceData referenceData, bool returnSession = false);
        Task<SeatMapEntity> GetSeatMapAsync(IEnumerable<string> segmentIds, ReferenceData referenceData, bool returnSession = false);
        Task SelectSeatsAsync(string seatNumber, string passengerFlightId, ReferenceData referenceData, bool returnSession = true);
        Task<IEnumerable<BoardingPassEntity>> CheckInAsync(List<string> passengerIds, List<string> passengerFlightIds, ReferenceData referenceData, bool returnSession = true);
        Task<IEnumerable<BoardingPassEntity>> GetBoardingPassesAsync(IEnumerable<string> passengerFlightIds, ReferenceData referenceData, bool returnSession = false);
        Task<byte[]> FormattedForWalletBoardingPassesAsync(string boardingPassJson);
        void ClearBoardingPassCache(string bookingRef);
    }
}
