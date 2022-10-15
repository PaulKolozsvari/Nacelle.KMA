using System;
using System.Collections.Generic;
using Nacelle.KMA.Core.Models.Entites;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace Nacelle.KMA.UI.Pages
{
    public partial class CountrySelectorPopup : PopupPage
    {
        public event EventHandler<object> CountrySelected;

        public CountrySelectorPopup(List<CountryEntity> countries)
        {
            InitializeComponent();

            BindingContext = countries;
        }

        private async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is CountryEntity selectedCountry)
            {
                CountrySelected?.Invoke(this, selectedCountry);

                await PopupNavigation.Instance.RemovePageAsync(this);
            }
        }
    }
}
