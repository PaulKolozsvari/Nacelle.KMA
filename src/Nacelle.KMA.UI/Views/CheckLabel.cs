using System;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Views
{
    public class CheckLabel: Label
    {
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.CreateAttached(nameof(IsChecked), typeof(bool), typeof(CheckLabel), false, propertyChanged: OnIsCheckedChanged);
        public static readonly BindableProperty CheckedColorProperty = BindableProperty.CreateAttached(nameof(CheckedColor), typeof(Color), typeof(CheckLabel), (Color)Application.Current.Resources["PrimaryGreen"]);
        public static readonly BindableProperty UnCheckedColorProperty = BindableProperty.CreateAttached(nameof(UnCheckedColor), typeof(Color), typeof(CheckLabel), (Color)Application.Current.Resources["PrimaryGrey"]);

        public CheckLabel()
        {
            FontFamily = (OnPlatform<string>)Application.Current.Resources["GlyphFontFamily"];
            Text = IconFont.CheckEmpty;
            var tapGestureRecognizer = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1
            };
            tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
            GestureRecognizers.Add(tapGestureRecognizer);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            IsChecked = !IsChecked;
        }

        private static void OnIsCheckedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (CheckLabel)bindable;
            view.IsChecked = (bool)newValue;
        }

        public Color CheckedColor
        {
            get => (Color)GetValue(CheckedColorProperty);
            set => SetValue(CheckedColorProperty, value);
        }

        public Color UnCheckedColor
        {
            get => (Color)GetValue(UnCheckedColorProperty);
            set => SetValue(UnCheckedColorProperty, value);
        }

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set
            {
                SetValue(IsCheckedProperty, value);
                SetValue(TextColorProperty, value ? CheckedColor : UnCheckedColor);
                SetValue(TextProperty, value ? IconFont.OkSquared : IconFont.CheckEmpty);
            }
        }
    }
}
