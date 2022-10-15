using Xamarin.Forms;

namespace Nacelle.KMA.UI.Views
{
    public partial class WeatherCardView : ContentView
    {
        public WeatherCardView()
        {
            InitializeComponent();
        }

        public static BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(WeatherCardView));

        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static BindableProperty SynopsisProperty = BindableProperty.Create(nameof(Synopsis), typeof(string), typeof(WeatherCardView));

        public string Synopsis
        {
            get => (string)GetValue(SynopsisProperty);
            set => SetValue(SynopsisProperty, value);
        }

        public static BindableProperty CityProperty = BindableProperty.Create(nameof(City), typeof(string), typeof(WeatherCardView));

        public string City
        {
            get => (string)GetValue(CityProperty);
            set => SetValue(CityProperty, value);
        }

        public static BindableProperty TemperatureProperty = BindableProperty.Create(nameof(Temperature), typeof(string), typeof(WeatherCardView));

        public string Temperature
        {
            get => (string)GetValue(TemperatureProperty);
            set => SetValue(TemperatureProperty, value);
        }
    }
}
