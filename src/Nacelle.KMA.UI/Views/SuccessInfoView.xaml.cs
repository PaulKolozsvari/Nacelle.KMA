using Xamarin.Forms;

namespace Nacelle.KMA.UI.Views
{
    public partial class SuccessInfoView : ContentView
    {
        public static readonly BindableProperty PrimaryTextProperty = BindableProperty.Create(
                                    nameof(PrimaryText),
                                    typeof(string),
                                    typeof(SuccessInfoView),
                                    null,
                                    BindingMode.OneWay,
                                    null,
                                    OnPrimaryTextChanged);

        public string PrimaryText
        {
            get => (string)GetValue(PrimaryTextProperty);
            set => SetValue(PrimaryTextProperty, value);
        }

        public static readonly BindableProperty SecondaryTextProperty = BindableProperty.Create(
                                    nameof(SecondaryText),
                                    typeof(string),
                                    typeof(SuccessInfoView),
                                    null,
                                    BindingMode.OneWay,
                                    null,
                                    OnSecondaryTextChanged);

        public string SecondaryText
        {
            get => (string)GetValue(SecondaryTextProperty);
            set => SetValue(SecondaryTextProperty, value);
        }

        public SuccessInfoView()
        {
            InitializeComponent();
        }

        private static void OnPrimaryTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SuccessInfoView successInfoView)
            {
                var text = newValue.ToString();

                successInfoView.primaryLabel.Text = text;
            }
        }

        private static void OnSecondaryTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SuccessInfoView successInfoView)
            {
                var text = newValue.ToString();

                successInfoView.secondaryLabel.Text = text;
                successInfoView.secondaryLabel.IsVisible = !string.IsNullOrWhiteSpace(text);
            }
        }
    }
}
