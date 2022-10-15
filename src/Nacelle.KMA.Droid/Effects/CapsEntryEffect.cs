using System.Linq;
using Android.Text;
using Android.Widget;
using Nacelle.KMA.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(CapsEntryEffect), "CapsEntryEffect")]
namespace Nacelle.KMA.Droid.Effects
{
    public class CapsEntryEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var editText = Control as EditText;
            editText.SetFilters(editText.GetFilters().Append(new InputFilterAllCaps()).ToArray());
        }

        protected override void OnDetached()
        {
        }
    }
}
