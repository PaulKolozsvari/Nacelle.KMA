using Android.OS;
using Android.Views;
using MvvmCross;
using MvvmCross.Platforms.Android;
using Nacelle.KMA.Core.Platform;
using Xamarin.Forms;

namespace Nacelle.KMA.Droid.Platform
{
    public class StatusBarStyleManager : IStatusBarStyleManager
    {
        public void SetDarkTheme() => ApplyTheme(0, Android.Graphics.Color.DarkCyan);

        public void SetLightTheme() => ApplyTheme((StatusBarVisibility)SystemUiFlags.LightStatusBar, Android.Graphics.Color.LightGreen);

        private void ApplyTheme(StatusBarVisibility statusBarVisibility, Android.Graphics.Color color)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var currentWindow = GetCurrentWindow();
                    currentWindow.DecorView.SystemUiVisibility = statusBarVisibility;
                    currentWindow.SetStatusBarColor(color);
                });
            }
        }

        private Window GetCurrentWindow()
        {
            var window = Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity.Window;

            // clear FLAG_TRANSLUCENT_STATUS flag:
            window.ClearFlags(WindowManagerFlags.TranslucentStatus);

            // add FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS flag to the window
            window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            return window;
        }
    }
}
