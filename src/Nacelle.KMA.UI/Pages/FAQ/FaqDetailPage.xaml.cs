using System;
using MvvmCross.Forms.Presenters.Attributes;
using Nacelle.KMA.Core.ViewModels;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Pages
{
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    public partial class FaqDetailPage : BaseContentPage<FaqDetailViewModel>
    {
        private bool _hasNavigated;

        public FaqDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

                //EmbeddedBrowser.Reload();
                //ViewModel.ReloadHtmlContent();
                //EmbeddedBrowser.Reload();
                //EmbeddedBrowser.GoBack();
        }

        private void Handle_Navigating(object sender, WebNavigatingEventArgs e)
        {
            var isAndroid = Device.RuntimePlatform == Device.Android;
            if (isAndroid)
            {
                _hasNavigated = true;
                e.Cancel = true;
                Device.OpenUri(new System.Uri(e.Url));
                return;
            }
            else
            {
                if (e.Url.StartsWith("file", StringComparison.InvariantCultureIgnoreCase))
                {
                    _hasNavigated = true;
                    return;
                }
            }

           if (_hasNavigated)
            {
                e.Cancel = true;
                Device.OpenUri(new Uri(e.Url));
            }
        }

        private void Handle_Navigated(object sender, WebNavigatedEventArgs e)
        {
            _hasNavigated = true;
        }
    }
}
