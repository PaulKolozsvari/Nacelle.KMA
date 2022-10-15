using System;
using MvvmCross.Forms.Views;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Pages
{
    public partial class SelectTravellerView : MvxContentView
    {
        public SelectTravellerView()
        {
            InitializeComponent();

            NavigationPage.SetBackButtonTitle(this, "");
        }

        private async void Handle_Tapped(object sender, EventArgs e)
        {
            if (Parent is SelectTravellerPopup parentView)
            {
                await PopupNavigation.Instance.RemovePageAsync(parentView);
            }
        }
    }
}
