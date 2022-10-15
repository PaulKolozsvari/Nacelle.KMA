#region Using Directives

using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using MonkeyCache;
using MonkeyCache.LiteDB;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Nacelle.Core.Services;
using Nacelle.KMA.API.Configs;
using Nacelle.KMA.API.Services;
using Nacelle.KMA.Core.Caching;
using Nacelle.KMA.Core.Managers;
using Nacelle.KMA.Core.Managers.Contracts;
using Nacelle.KMA.Core.Repositories;
using Nacelle.KMA.Core.Validators;
using Nacelle.KMA.Core.ViewModels;
using Xamarin.Essentials;

#endregion //Using Directives

namespace Nacelle.KMA.Core
{
    public class App : MvxApplication
    {
        #region Methods

        public override void Initialize()
        {
            ExperimentalFeatures.Enable(ExperimentalFeatures.ShareFileRequest);

            SetupMonkeyCache();
            RegisterConfigs();
            RegisterServices();
            RegisterManagers();

            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IViewModelValidator, ViewModelValidator>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IBookingRepository, BookingRepository>();

            RegisterAppStart<MainViewModel>();
        }

        public override async Task Startup()
        {
            await base.Startup();

            var dataMigrationManager = Mvx.IoCProvider.Resolve<IDataMigrationManager>();
            await dataMigrationManager.MigrateDataAsync();
        }

        private void SetupMonkeyCache()
        {
            Barrel.ApplicationId = Constants.MonkeyCacheApplicationId;
            Mvx.IoCProvider.RegisterSingleton<IBarrel>(Barrel.Current);
        }

        private void RegisterConfigs()
        {
            Mvx.IoCProvider.RegisterSingleton<TibcoConfig>(() => new TibcoConfig());
            Mvx.IoCProvider.RegisterSingleton<OpsApiConfig>(() => new OpsApiConfig());
            Mvx.IoCProvider.RegisterSingleton<NotificationsApiConfig>(() => new NotificationsApiConfig());
        }

        private void RegisterServices()
        {
            // NB: Please don't comment out this "live" HTTP Service, let the stub overwrite the IOC registration if need be in the line after that
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IHttpClientService, HttpClientService>();
#if DEBUG
            //Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IHttpClientService, API.Stub.StubHttpClientService>();
#endif
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<ITibcoService, TibcoService>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IOpsApiService, OpsApiService>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<INotificationsApiService, NotificationsApiService>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<ICacheService, MonkeyCacheService>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IAppSettings, AppSettings>();
        }

        private void RegisterManagers()
        {
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IBookingManager, BookingManager>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<ICheckInManager, CheckInManager>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IWeatherManager, WeatherManager>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IAirportManager, AirportManager>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IConnectivityManager, ConnectivityManager>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<INotificationsManager, NotificationsManager>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IDataMigrationManager, DataMigrationManager>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<ICountryManager, CountryManager>();
        }

        #endregion //Methods

        #region Properties

#if DEBUG
        public static bool IsStubMode => Mvx.IoCProvider.Resolve<IHttpClientService>().GetType().Name.Equals("StubHttpClientService");
#endif

        #endregion //Properties

    }
}
