using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Views
{
    public partial class FindBookingView : ContentView
    {
        // Description

        public static BindableProperty DescriptionProperty = BindableProperty.Create(
            nameof(Description),
            typeof(string),
            typeof(FindBookingView),
            string.Empty,
            BindingMode.TwoWay);

        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        // BookingReference

        public static BindableProperty BookingReferenceProperty = BindableProperty.Create(
            nameof(BookingReference),
            typeof(string),
            typeof(FindBookingView),
            string.Empty,
            BindingMode.TwoWay);

        public string BookingReference
        {
            get => (string)GetValue(BookingReferenceProperty);
            set => SetValue(BookingReferenceProperty, value);
        }

        // ValidationPropertyNameBookingReference

        public static BindableProperty ValidationPropertyNameBookingReferenceProperty = BindableProperty.Create(
            nameof(ValidationPropertyNameBookingReference),
            typeof(string),
            typeof(FindBookingView),
            string.Empty,
            BindingMode.TwoWay);

        public string ValidationPropertyNameBookingReference
        {
            get => (string)GetValue(ValidationPropertyNameBookingReferenceProperty);
            set => SetValue(ValidationPropertyNameBookingReferenceProperty, value);
        }

        // LastName

        public static BindableProperty LastNameProperty = BindableProperty.Create(
            nameof(LastName),
            typeof(string),
            typeof(FindBookingView),
            string.Empty,
            BindingMode.TwoWay);

        public string LastName
        {
            get => (string)GetValue(LastNameProperty);
            set => SetValue(LastNameProperty, value);
        }

        // ValidationPropertyNameBookingReferenceProperty

        public static BindableProperty ValidationPropertyNameLastNameProperty = BindableProperty.Create(
            nameof(ValidationPropertyNameLastName),
            typeof(string),
            typeof(FindBookingView),
            string.Empty,
            BindingMode.TwoWay);

        public string ValidationPropertyNameLastName
        {
            get => (string)GetValue(ValidationPropertyNameLastNameProperty);
            set => SetValue(ValidationPropertyNameLastNameProperty, value);
        }

        // ButtonTitle

        public static BindableProperty ButtonTitleProperty = BindableProperty.Create(
            nameof(ButtonTitle),
            typeof(string),
            typeof(FindBookingView),
            string.Empty,
            BindingMode.TwoWay);

        public string ButtonTitle
        {
            get => (string)GetValue(ButtonTitleProperty);
            set => SetValue(ButtonTitleProperty, value);
        }

        // FindBookingCommand

        public static readonly BindableProperty FindBookingCommandProperty = BindableProperty.Create(
            nameof(FindBookingCommand),
            typeof(ICommand),
            typeof(FindBookingView));

        public ICommand FindBookingCommand
        {
            get => (ICommand)GetValue(FindBookingCommandProperty);
            set => SetValue(FindBookingCommandProperty, value);
        }

        public FindBookingView()
        {
            InitializeComponent();

            BookingReferenceEntry.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeCharacter);
        }

        private void OnBookingReferenceCompleted(object sender, EventArgs e)
        {
            LastNameEntry.Focus();
        }

        private void OnLastNameCompleted(object sender, EventArgs e)
        {
            if (FindBookingCommand is ICommand command &&
                command.CanExecute(sender))
            {
                command.Execute(sender);
            }
        }
    }
}
