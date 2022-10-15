using System;
using Xamarin.Forms;
using System.Diagnostics;
using System.Timers;

namespace Nacelle.KMA.UI.Views
{
    public class WebViewEx: WebView
    {
        private bool _hasNavigated;

        public WebViewEx()
        {
            this.Navigating += Handle_Navigating;
            this.Navigated += Handle_Navigated;
        }

        public Action OnHasNavigated { get; set; }

        private void Handle_Navigating(object sender, WebNavigatingEventArgs e)
        {
            Debug.WriteLine("Navigating 0");
            if (string.IsNullOrEmpty(e.Url))
            {
                return;
            }
            Debug.WriteLine("Navigating 1");
            var isAndroid = Device.RuntimePlatform == Device.Android;
            if (isAndroid)
            {
                DoOnHasNavigated();
                e.Cancel = true;
                Device.OpenUri(new System.Uri(e.Url));
                return;
            }
            else
            {
                if (e.Url.StartsWith("file", StringComparison.InvariantCultureIgnoreCase))
                {
                    DoOnHasNavigated();
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
            DoOnHasNavigated();
        }

        private void DoOnHasNavigated()
        {
            _hasNavigated = true;
            if (OnHasNavigated != null)
            {
                OnHasNavigated.Invoke();
            }
        }

    }
}
