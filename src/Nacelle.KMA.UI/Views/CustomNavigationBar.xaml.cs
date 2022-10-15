using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Windows.Input;

namespace Nacelle.KMA.UI.Views
{
    public partial class CustomNavigationBar : ContentView
    {
        public CustomNavigationBar()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TitleProperty = BindableProperty.CreateAttached(nameof(Title), typeof(string), typeof(CustomNavigationBar), null);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static BindableProperty IsBackButtonVisibleProperty = BindableProperty.Create(nameof(IsBackButtonVisible), typeof(bool), typeof(CustomNavigationBar), defaultValue: false);

        public bool IsBackButtonVisible
        {
            get => (bool)GetValue(IsBackButtonVisibleProperty);
            set => SetValue(IsBackButtonVisibleProperty, value);
        }

        public Label Label => this.TitleLabel;
    }
}
