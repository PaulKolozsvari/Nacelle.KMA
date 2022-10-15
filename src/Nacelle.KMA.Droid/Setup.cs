#region Using Directives

using Android.App;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.IoC;
using Nacelle.KMA.Core.Analytics;
using Nacelle.KMA.Core.Platform;
using Nacelle.KMA.Droid.Platform;
using Nacelle.KMA.Droid.Plugins;
using Nacelle.KMA.Droid.Services;
using Nacelle.KMA.UI.Plugins;
using Nacelle.KMA.UI.Services;

#if DEBUG
[assembly: Application(Debuggable = true)]
#else
[assembly: Application(Debuggable = false)]
#endif

#endregion //Using Directives

namespace Nacelle.KMA.Droid
{
    public class Setup : MvxFormsAndroidSetup<Core.App, UI.App>
    {
        #region Methods

        protected override void InitializeIoC()
        {
            base.InitializeIoC();

            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IPlatformPath, PlatformPath>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IProgressActivityService, ProgressActivityService>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IAlertService, AlertService>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IToastService, ToastService>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IScreenshotService, ScreenshotService>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IBoardingPassPdfGenerator, BoardingPassPdfGenerator>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IBarcodeService, BarcodeService>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IAnalyticsService, FirebaseAnalyticsService>();

            //TODO: Might not need to register IStatusBarStyleManager for android as status bar constantly has black background.
            //TODO: Confirm and revisit as implementation is implemented and tested.
            //Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IStatusBarStyleManager, StatusBarStyleManager>();
        }

        #endregion //Methods
    }
}
