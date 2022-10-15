using System.Threading.Tasks;
using Nacelle.KMA.API.Services;

namespace Nacelle.KMA.Core.Managers
{
    public class NotificationsManager : INotificationsManager
    {
        private readonly INotificationsApiService _apiService;

        public NotificationsManager(INotificationsApiService apiService)
        {
            _apiService = apiService;
        }

        public Task<bool> RegisterPushNotificationsTokenAsync(string pnToken, string deviceType)
        {
            return _apiService.RegisterPushNotificationsTokenAsync(pnToken, deviceType);
        }
    }
}
