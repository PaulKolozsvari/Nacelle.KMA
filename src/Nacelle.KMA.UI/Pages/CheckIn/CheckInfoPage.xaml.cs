#region Using Directives

using System.Timers;
using MvvmCross.Forms.Presenters.Attributes;
using Nacelle.KMA.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Essentials;
using System;

#endregion //Using Directives

namespace Nacelle.KMA.UI.Pages
{
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    public partial class CheckInfoPage : BaseContentPage<CheckInfoViewModel>
    {
        #region Constructors

        public CheckInfoPage()
        {
            InitializeComponent();

            NavigationPage.SetBackButtonTitle(this, "");

            _timer = new Timer(1);
            _timer.Elapsed += TimerElapsedAsync;
        }

        #endregion //Constructors

        #region Fields

        private Timer _timer;

        #endregion //Fields

        #region Methods

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (_timer != null)
            {
                _timer.Start();
            }
        }

        protected override void OnDisappearing()
        {
            if (_timer != null)
            {
                _timer.Stop();
            }
            base.OnDisappearing();
        }

        private void TimerElapsedAsync(object sender, ElapsedEventArgs e)
        {
            if (!(EmbeddedBrowser.Source is HtmlWebViewSource source) || string.IsNullOrEmpty(source.Html))
            {
                return;
            }
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (_timer == null || EmbeddedBrowser == null)
                {
                    return;
                }
                var result = await EmbeddedBrowser.EvaluateJavaScriptAsync("hasScrolledCallBack();");
                if (result != null && bool.Parse(result))
                {
                    _timer.Stop();
                    ViewModel.IsTermsScrolled = true;
                }
            });
        }

        #endregion //Methods
    }
}
