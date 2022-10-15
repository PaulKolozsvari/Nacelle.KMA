using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Views
{
    public partial class ButtonSelector : ContentView
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
                                    nameof(ItemsSource),
                                    typeof(IEnumerable),
                                    typeof(ButtonSelector),
                                    null,
                                    BindingMode.OneWay,
                                    null,
                                    OnItemsSourceChanged);

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(
                                    nameof(SelectedIndex),
                                    typeof(int),
                                    typeof(ButtonSelector),
                                    -1,
                                    BindingMode.TwoWay);

        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        public ButtonSelector()
        {
            InitializeComponent();
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ButtonSelector buttonSelector && newValue is IEnumerable items)
            {
                var counter = 0;
                var itemsSource = new Dictionary<int, object>();

                foreach (var item in items)
                {
                    itemsSource.Add(counter++, item);
                }

                BindableLayout.SetItemsSource(buttonSelector.buttonSelectorLayout, itemsSource);
            }
        }

        private void OnButtonSelectorItemTapped(object sender, EventArgs e)
        {
            if (sender is ButtonSelectorItemView buttonSelectorItemView)
            {
                SelectedIndex = buttonSelectorItemView.Index;
            }
        }
    }
}
