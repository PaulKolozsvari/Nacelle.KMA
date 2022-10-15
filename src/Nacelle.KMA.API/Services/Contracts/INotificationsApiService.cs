using System.Threading.Tasks;

namespace Nacelle.KMA.API.Services
{
    public interface INotificationsApiService
    {
        Task<bool> RegisterPushNotificationsTokenAsync(string pnToken, string deviceType);

        Task<byte[]> BoardingPassWalletBytesAsync(string boardingPassJson);
    }
}
