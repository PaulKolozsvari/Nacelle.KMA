using Android.Support.Design.BottomNavigation;
using Android.Support.Design.Widget;
using Android.Views;
using Nacelle.KMA.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

// Taken from: https://montemagno.com/xamarin-forms-fully-customize-bottom-tabs-on-android-turn-off-shifting/
[assembly: ResolutionGroupName("Nacelle")]
[assembly: ExportEffect(typeof(NoShiftEffect), "NoShiftEffect")]
namespace Nacelle.KMA.Droid.Effects
{
    public class NoShiftEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (!(Container.GetChildAt(0) is ViewGroup layout))
                return;

            if (!(layout.GetChildAt(1) is BottomNavigationView bottomNavigationView))
                return;

            // This is what we set to adjust if the shifting happens
            bottomNavigationView.LabelVisibilityMode = LabelVisibilityMode.LabelVisibilityLabeled;
        }

        protected override void OnDetached()
        {
        }
    }
}
