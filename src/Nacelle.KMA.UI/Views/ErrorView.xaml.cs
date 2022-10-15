using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Views
{
    public partial class ErrorView : ContentView
    {
        // ErrorMessage

        public static BindableProperty ErrorMessageProperty = BindableProperty.Create(
            nameof(ErrorMessage),
            typeof(string),
            typeof(ErrorView),
            string.Empty,
            BindingMode.TwoWay);

        public string ErrorMessage
        {
            get => (string)GetValue(ErrorMessageProperty);
            set => SetValue(ErrorMessageProperty, value);
        }

        public ErrorView()
        {
            InitializeComponent();
        }
    }
}
