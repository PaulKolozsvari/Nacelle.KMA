using Xamarin.Forms;

namespace Nacelle.KMA.UI.Views
{
    public class KMALabel : Label
    {
        public static readonly BindableProperty KerningProperty = BindableProperty.Create(
           propertyName: nameof(Kerning),
           returnType: typeof(float),
           declaringType: typeof(KMALabel),
           defaultValue: 0.0f,
           defaultBindingMode: BindingMode.TwoWay);

        public float Kerning
        {
            get => (float)GetValue(KerningProperty);
            set => SetValue(KerningProperty, value);
        }
    }
}
