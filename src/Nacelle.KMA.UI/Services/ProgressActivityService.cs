using Nacelle.KMA.Core.Platform;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Services
{
    public class ProgressActivityService : IProgressActivityService
    {
        private bool _isShowing;

        private PopupPage _popupPage;

        public void Show()
        {
            if (!_isShowing)
            {
                _isShowing = true;

                PopupNavigation.Instance.PushAsync(Popup);
            }
        }

        public void Hide()
        {
            if (_isShowing)
            {
                PopupNavigation.Instance.PopAllAsync();

                _isShowing = false;
            }
        }

        public PopupPage Popup
        {
            get 
            { 
                if (_popupPage == null)
                {
                    _popupPage = new PopupPage
                    {
                        Visual = VisualMarker.Material,
                        CloseWhenBackgroundIsClicked = false,
                        Content = new ActivityIndicator
                        { 
                            HorizontalOptions = LayoutOptions.Center, 
                            VerticalOptions = LayoutOptions.Center, 
                            IsRunning = true, 
                            Color = Color.GreenYellow, 
                            HeightRequest = 60, 
                            WidthRequest = 60 
                        }
                    };
                }

                return _popupPage;
            }
        }
    }
}
