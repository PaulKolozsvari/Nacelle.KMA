using System.Threading.Tasks;

namespace Nacelle.KMA.Core.Managers
{
    public interface INotificationsManager
    {
        Task<bool> RegisterPushNotificationsTokenAsync(string pnToken, string deviceType);
    }
}
