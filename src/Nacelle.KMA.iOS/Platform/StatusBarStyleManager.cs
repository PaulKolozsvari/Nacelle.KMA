using Nacelle.KMA.Core.Platform;
using UIKit;
using Xamarin.Forms;

namespace Nacelle.KMA.iOS.Platform
{
    public class StatusBarStyleManager : IStatusBarStyleManager
    {
        public void SetDarkTheme() => ApplyTheme(UIStatusBarStyle.LightContent);

        public void SetLightTheme() => ApplyTheme(UIStatusBarStyle.Default);

        private void ApplyTheme(UIStatusBarStyle statusBarStyle)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                UIApplication.SharedApplication.SetStatusBarStyle(statusBarStyle, false);
                GetCurrentViewController().SetNeedsStatusBarAppearanceUpdate();
            });
        }

        private UIViewController GetCurrentViewController()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var viewController = window.RootViewController;
            while (viewController.PresentedViewController != null)
                viewController = viewController.PresentedViewController;
            return viewController;
        }
    }
}
