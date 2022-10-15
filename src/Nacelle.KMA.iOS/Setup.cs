#region Using Directives

using MvvmCross;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.IoC;
using Nacelle.KMA.Core.Analytics;
using Nacelle.KMA.Core.Platform;
using Nacelle.KMA.iOS.Platform;
using Nacelle.KMA.iOS.Plugins;
using Nacelle.KMA.iOS.Services;
using Nacelle.KMA.UI.Plugins;
using Nacelle.KMA.UI.Services;

#endregion //Using Directives

namespace Nacelle.KMA.iOS
{
    public class Setup : MvxFormsIosSetup<Core.App, UI.App>
    {
        #region Methods

        protected override void InitializeIoC()
        {
            base.InitializeIoC();

            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IPlatformPath, PlatformPath>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IProgressActivityService, ProgressActivityService>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IStatusBarStyleManager, StatusBarStyleManager>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IAlertService, AlertService>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IToastService, ToastService>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IScreenshotService, ScreenshotService>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IPassKitService, BoardingPassWallet>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IBoardingPassPdfGenerator, BoardingPassPdfGenerator>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IBarcodeService, BarcodeService>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IAnalyticsService, FirebaseAnalyticsService>();
        }

        #endregion //Methods
    }
}
