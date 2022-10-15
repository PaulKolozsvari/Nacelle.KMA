using System;
using MvvmCross.Forms.Presenters.Attributes;
using Nacelle.KMA.Core.ViewModels;
using Nacelle.KMA.UI.Views;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Pages
{
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    public partial class CheckInSuccessPage : BaseContentPage<CheckInSuccessViewModel>
    {
        public CheckInSuccessPage()
        {
            InitializeComponent();

            NavigationPage.SetBackButtonTitle(this, "");
        }

        private async void Share_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new SelectShareTravellersPopup(ViewModel));
        }
    }
}
