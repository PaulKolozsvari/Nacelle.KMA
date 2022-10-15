using Xamarin.Forms;

namespace Nacelle.KMA.UI.Views
{
    public partial class InfoView : ContentView
    {
        // InfoMessage

        public static BindableProperty InfoMessageProperty = BindableProperty.Create(
            nameof(InfoMessage),
            typeof(string),
            typeof(InfoView),
            string.Empty,
            BindingMode.TwoWay);

        public string InfoMessage
        {
            get => (string)GetValue(InfoMessageProperty);
            set => SetValue(InfoMessageProperty, value);
        }

        public InfoView()
        {
            InitializeComponent();
        }
    }
}
