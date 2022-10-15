using System.Threading.Tasks;

namespace Nacelle.KMA.Core.Platform
{
    public interface IPassKitService
    {
        Task AddToWallet(byte[] passJson);
    }
}
