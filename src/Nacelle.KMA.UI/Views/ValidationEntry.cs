using System.Diagnostics;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Views
{
    public class ValidationEntry: Entry
    {
        public static readonly BindableProperty IsValidProperty = BindableProperty.CreateAttached("IsValid", typeof(bool), typeof(ValidationEntry), true, propertyChanged: OnIsValidChanged);
        public static readonly BindableProperty ValidColorProperty = BindableProperty.CreateAttached("ValidColor", typeof(Color), typeof(ValidationEntry), (Color)Application.Current.Resources["PrimaryGrey"]);
        public static readonly BindableProperty InvalidColorProperty = BindableProperty.CreateAttached("InvalidColor", typeof(Color), typeof(ValidationEntry), (Color)Application.Current.Resources["ErrorMedium"]);

        private static void OnIsValidChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (ValidationEntry)bindable;
            view.IsValid = (bool)newValue;
        }

        public Color ValidColor
        {
            get => (Color)GetValue(ValidColorProperty);
            set => SetValue(ValidColorProperty, value);
        }

        public Color InvalidColor
        {
            get => (Color)GetValue(InvalidColorProperty);
            set => SetValue(InvalidColorProperty, value);
        }

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set
            {
                SetValue(IsValidProperty, value);
                SetValue(PlaceholderColorProperty, value ? ValidColor : InvalidColor);
            }
        }
    }
}
