using System.Collections.Generic;
using MvvmCross.Plugin.Messenger;
using Nacelle.KMA.Core.Enums;

namespace Nacelle.KMA.Core.Messages
{
    public class ConnectivityMessage : MvxMessage
    {
        public ConnectivityMessage(object sender, NetworkAccess networkAccess, IReadOnlyList<ConnectionProfile> connectionProfiles) : base(sender)
        {
            NetworkAccess = networkAccess;
            ConnectionProfiles = connectionProfiles;
        }

        public NetworkAccess NetworkAccess { get; }
        public IReadOnlyList<ConnectionProfile> ConnectionProfiles { get; }
    }
}
