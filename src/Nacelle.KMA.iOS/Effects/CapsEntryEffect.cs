using Nacelle.KMA.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("Nacelle")]
[assembly: ExportEffect(typeof(CapsEntryEffect), "CapsEntryEffect")]
namespace Nacelle.KMA.iOS.Effects
{
    public class CapsEntryEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var textField = Control as UITextField;
            textField.AutocapitalizationType = UITextAutocapitalizationType.AllCharacters;
            textField.AutocorrectionType = UITextAutocorrectionType.No;
        }

        protected override void OnDetached()
        {
        }
    }
}
