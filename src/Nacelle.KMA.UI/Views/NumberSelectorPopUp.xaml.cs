using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Views
{
    public partial class NumberSelectorPopUp : PopupPage
    {
        public event EventHandler<int> NumberSelected;

        public NumberSelectorPopUp(List<int> numbers)
        {
            InitializeComponent();

            BindingContext = numbers;
        }

        private async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is int selectedNumber)
            {
                NumberSelected?.Invoke(this, selectedNumber);

                await PopupNavigation.Instance.RemovePageAsync(this);
            }
        }
    }
}
