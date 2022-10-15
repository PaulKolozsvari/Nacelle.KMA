using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Views
{
    public partial class ButtonSelectorItemView : ContentView
    {
        public event EventHandler Tapped;

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
                                    nameof(Text),
                                    typeof(string),
                                    typeof(ButtonSelectorItemView),
                                    null,
                                    BindingMode.OneWay,
                                    null,
                                    OnTextChanged);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty IndexProperty = BindableProperty.Create(
                                    nameof(Index),
                                    typeof(int),
                                    typeof(ButtonSelectorItemView),
                                    null,
                                    BindingMode.OneWay);

        public int Index
        {
            get => (int)GetValue(IndexProperty);
            set => SetValue(IndexProperty, value);
        }

        public static readonly BindableProperty IsActiveProperty = BindableProperty.Create(
                                    nameof(IsActive),
                                    typeof(bool),
                                    typeof(ButtonSelectorItemView),
                                    null,
                                    BindingMode.OneWay,
                                    null,
                                    OnIsActiveChanged);

        public bool IsActive
        {
            get => (bool)GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        public static readonly BindableProperty ItemTappedCommandProperty = BindableProperty.Create(
                                    nameof(ItemTappedCommand),
                                    typeof(ICommand),
                                    typeof(ButtonSelectorItemView));

        public ICommand ItemTappedCommand
        {
            get => (ICommand)GetValue(ItemTappedCommandProperty);
            set => SetValue(ItemTappedCommandProperty, value);
        }

        public ButtonSelectorItemView()
        {
            InitializeComponent();
        }

        private static void OnIsActiveChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ButtonSelectorItemView buttonSelectorItemView)
            {
                buttonSelectorItemView.ContainerFrame.BackgroundColor = (bool)newValue ? (Color)Application.Current.Resources["PrimaryBlue"] : Color.White;
                buttonSelectorItemView.TitleLabel.TextColor = (bool)newValue ? Color.White : (Color)Application.Current.Resources["PrimaryBlue"];
            }
        }

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ButtonSelectorItemView buttonSelectorItemView)
            {
                buttonSelectorItemView.TitleLabel.Text = newValue.ToString();
            }
        }

        private void OnTapped(object sender, EventArgs e)
        {
            Tapped?.Invoke(this, EventArgs.Empty);
        }
    }
}
