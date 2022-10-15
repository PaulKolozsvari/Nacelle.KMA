using System;
using MvvmCross.Forms.Presenters.Attributes;
using Nacelle.KMA.Core.ViewModels;
using Rg.Plugins.Popup.Services;

namespace Nacelle.KMA.UI.Pages
{
    [MvxModalPresentation(WrapInNavigationPage = true)]
    public partial class SelectSeatPage : BaseContentPage<SelectSeatViewModel>
    {
        public SelectSeatPage()
        {
            InitializeComponent();
        }

        private async void TravellerSelectorTapped(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new SelectTravellerPopup(ViewModel));
        }
    }
}
