using System;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Views
{
    public partial class FlightToAndFromHeaderCardView : ContentView
    {
        public FlightToAndFromHeaderCardView()
        {
            InitializeComponent();
        }

        public static BindableProperty ToProperty = BindableProperty.Create(nameof(To), typeof(string), typeof(FlightToAndFromHeaderCardView));

        public string To
        {
            get => (string)GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }

        public static BindableProperty FromProperty = BindableProperty.Create(nameof(From), typeof(string), typeof(FlightToAndFromHeaderCardView));

        public string From
        {
            get => (string)GetValue(FromProperty);
            set => SetValue(FromProperty, value);
        }

        public static BindableProperty DateFromProperty = BindableProperty.Create(nameof(DateFrom), typeof(DateTime), typeof(FlightToAndFromHeaderCardView));

        public DateTime DateFrom
        {
            get => (DateTime)GetValue(DateFromProperty);
            set => SetValue(DateFromProperty, value);
        }

        public static BindableProperty DateToProperty = BindableProperty.Create(nameof(DateTo), typeof(DateTime), typeof(FlightToAndFromHeaderCardView));

        public DateTime DateTo
        {
            get => (DateTime)GetValue(DateToProperty);
            set => SetValue(DateToProperty, value);
        }
    }
}
