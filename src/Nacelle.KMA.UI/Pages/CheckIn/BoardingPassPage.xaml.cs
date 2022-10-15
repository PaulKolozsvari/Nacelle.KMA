using System;
using MvvmCross.Forms.Presenters.Attributes;
using Nacelle.KMA.Core.ViewModels;
using Nacelle.KMA.UI.Views;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace Nacelle.KMA.UI.Pages
{
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    public partial class BoardingPassPage : BaseContentPage<BoardingPassViewModel>
    {
        private Color _previousColor;

        public BoardingPassPage()
        {
            InitializeComponent();

            NavigationPage.SetBackButtonTitle(this, "");

            BoardingPassesCoverFlow.Opacity = 0;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                navigationPage.BarTextColor = Color.White;
                _previousColor = navigationPage.BarBackgroundColor;
                navigationPage.BarBackgroundColor = (Color)Application.Current.Resources["Blue2"];
            }

            // There is a slight shuffle of the card, so fading it in rather.
            var animation = new Animation(v => BoardingPassesCoverFlow.Opacity = v, 0, 1);
            animation.Commit(BoardingPassesCoverFlow, "FadeIn", 16, 500, Easing.CubicIn, null, () => false);
        }

        protected override void OnDisappearing()
        {
            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                navigationPage.BarTextColor = Color.White;
                navigationPage.BarBackgroundColor = _previousColor;
            }

            base.OnDisappearing();
        }

        private async void OnQrCodeTapped(object sender, EventArgs e)
        {
            if (sender is ZXingBarcodeImageView barcodeImageView)
            {
                var qrCode = barcodeImageView.BarcodeValue;

                await PopupNavigation.Instance.PushAsync(new QrCodePopup(qrCode));
            }
        }

        private async void Share_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new SelectShareTravellersPopup(ViewModel));
        }

        public bool IsAddToWalletButtonVisible => Device.Idiom == TargetIdiom.Phone && Device.RuntimePlatform == Device.iOS;
    }
}
