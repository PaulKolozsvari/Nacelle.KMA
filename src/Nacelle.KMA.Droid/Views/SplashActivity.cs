using System.Threading.Tasks;
using FFImageLoading.Forms.Platform;
using FFImageLoading.Svg.Forms;
using Android.App;
using Android.OS;
using MvvmCross.Forms.Platforms.Android.Views;

namespace Nacelle.KMA.Droid.Views
{
    [Activity(
       NoHistory = true,
       MainLauncher = true,
       Label = "@string/app_name",
       Theme = "@style/AppTheme.Splash",
       Icon = "@mipmap/ic_launcher",
       RoundIcon = "@mipmap/ic_launcher_round")]
    public class SplashActivity : MvxFormsSplashScreenAppCompatActivity<Setup, Core.App, UI.App>
    {
        protected override Task RunAppStartAsync(Bundle bundle)
        {
            StartActivity(typeof(AppActivity));
            return Task.CompletedTask;
        }
    }
}
