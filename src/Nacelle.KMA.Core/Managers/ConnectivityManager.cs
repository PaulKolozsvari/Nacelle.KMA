using System.Collections.Generic;
using Nacelle.KMA.Core.Managers.Contracts;
using Xamarin.Essentials;
using System.Linq;
using MvvmCross.Plugin.Messenger;
using MvvmCross;
using Nacelle.KMA.Core.Messages;
using Nacelle.KMA.Core.Analytics;

namespace Nacelle.KMA.Core.Managers
{
    public class ConnectivityManager : IConnectivityManager
    {
        private readonly IMvxMessenger _messenger;

        public ConnectivityManager(IMvxMessenger messenger)
        {
            _messenger = messenger;
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        ~ConnectivityManager()
        {
            Connectivity.ConnectivityChanged -= OnConnectivityChanged;
        }

        public Enums.NetworkAccess NetworkAccess => GetNetworkAccess();

        public IReadOnlyList<Enums.ConnectionProfile> ConnectionProfiles => GetConnnectionProfiles();

        private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs eventArgs)
        {
           _messenger.Publish(new ConnectivityMessage(this, NetworkAccess, ConnectionProfiles));
        }

        private Enums.NetworkAccess GetNetworkAccess()
        {
            switch (Connectivity.NetworkAccess)
            {
                case Xamarin.Essentials.NetworkAccess.Unknown:
                    Mvx.IoCProvider.Resolve<IAnalyticsService>().LogEvent(Constants.Analytics.Events.Offline);
                    return Enums.NetworkAccess.Unknown;
                case Xamarin.Essentials.NetworkAccess.None:
                    Mvx.IoCProvider.Resolve<IAnalyticsService>().LogEvent(Constants.Analytics.Events.Offline);
                    return Enums.NetworkAccess.None;
                case Xamarin.Essentials.NetworkAccess.Local:
                    Mvx.IoCProvider.Resolve<IAnalyticsService>().LogEvent(Constants.Analytics.Events.Offline);
                    return Enums.NetworkAccess.Local;
                case Xamarin.Essentials.NetworkAccess.ConstrainedInternet:
                    Mvx.IoCProvider.Resolve<IAnalyticsService>().LogEvent(Constants.Analytics.Events.Offline);
                    return Enums.NetworkAccess.ConstrainedInternet;
                default:
                    Mvx.IoCProvider.Resolve<IAnalyticsService>().LogEvent(Constants.Analytics.Events.Online);
                    return Enums.NetworkAccess.Internet;
            }
        }

        private IReadOnlyList<Enums.ConnectionProfile> GetConnnectionProfiles()
        {
           return Connectivity.ConnectionProfiles.SelectMany((arg) => MatchConnectionProfiles(arg)).ToList();
        }

        private List<Enums.ConnectionProfile> MatchConnectionProfiles(Xamarin.Essentials.ConnectionProfile connectionProfile)
        {
            var localConnectionProfiles = new List<Enums.ConnectionProfile>();
            switch(connectionProfile)
            {
                case Xamarin.Essentials.ConnectionProfile.Unknown:
                    localConnectionProfiles.Add(Enums.ConnectionProfile.Unknown);
                    break;
                case Xamarin.Essentials.ConnectionProfile.Bluetooth:
                    localConnectionProfiles.Add(Enums.ConnectionProfile.Bluetooth);
                    break;
                case Xamarin.Essentials.ConnectionProfile.Ethernet:
                    localConnectionProfiles.Add(Enums.ConnectionProfile.Unknown);
                    break;
                case Xamarin.Essentials.ConnectionProfile.WiFi:
                    localConnectionProfiles.Add(Enums.ConnectionProfile.WiFi);
                    break;
                default:
                    localConnectionProfiles.Add(Enums.ConnectionProfile.Cellular);
                    break;
            }
            return localConnectionProfiles;
        }
    }
}
