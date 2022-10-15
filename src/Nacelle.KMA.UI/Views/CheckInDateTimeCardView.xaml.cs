using Xamarin.Forms;

namespace Nacelle.KMA.UI.Views
{
    public partial class CheckInDateTimeCardView : ContentView
    {
        public CheckInDateTimeCardView()
        {
            InitializeComponent();
        }

        public static BindableProperty MessageProperty = BindableProperty.Create(nameof(Message), typeof(string), typeof(CheckInDateTimeCardView));

        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public static BindableProperty DateOrTimeProperty = BindableProperty.Create(nameof(DateOrTime), typeof(string), typeof(CheckInDateTimeCardView));

        public string DateOrTime
        {
            get => (string)GetValue(DateOrTimeProperty);
            set => SetValue(DateOrTimeProperty, value);
        }

        public static BindableProperty CheckInTimeProperty = BindableProperty.Create(nameof(CheckInTime), typeof(string), typeof(CheckInDateTimeCardView));

        public string CheckInTime
        {
            get => (string)GetValue(CheckInTimeProperty);
            set => SetValue(CheckInTimeProperty, value);
        }
    }
}
