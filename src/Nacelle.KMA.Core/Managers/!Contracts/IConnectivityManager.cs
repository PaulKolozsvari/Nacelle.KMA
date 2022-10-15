using System.Collections.Generic;
using Nacelle.KMA.Core.Enums;

namespace Nacelle.KMA.Core.Managers.Contracts
{
    public interface IConnectivityManager
    {
        NetworkAccess NetworkAccess { get; }
        IReadOnlyList<ConnectionProfile> ConnectionProfiles { get; }
    }
}
