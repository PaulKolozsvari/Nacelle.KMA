using System;
using System.Threading.Tasks;

namespace Nacelle.KMA.Core.Platform
{
    public interface IToastService
    {
        Task Show(string message, bool isError);
    }
}
