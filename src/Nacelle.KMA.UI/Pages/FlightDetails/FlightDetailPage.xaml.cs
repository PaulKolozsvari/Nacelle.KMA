#region Using Directives

using System.Collections.Generic;
using System.Linq;
using MvvmCross.Forms.Presenters.Attributes;
using Nacelle.KMA.Core.ViewModels;
using Xamarin.Forms;

#endregion //Using Directives

namespace Nacelle.KMA.UI.Pages
{
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    public partial class FlightDetailPage : RefreshableContentPage<FlightDetailViewModel>
    {
        #region Constructors

        public FlightDetailPage()
        {
            InitializeComponent();
            base.PullToRefresh = PullToRefresh;
        }

        #endregion //Constructors

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.ScrollToTop += ViewModel_ScrollToTop;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.ScrollToTop -= ViewModel_ScrollToTop;
        }

        private async void ViewModel_ScrollToTop()
        {
            PullToRefresh.IsPullToRefreshEnabled = false;
            await this.ScrollView.ScrollToAsync(this.ScrollView, ScrollToPosition.Start, true);
            PullToRefresh.IsPullToRefreshEnabled = true;
        }

        #region Event Handlers

        private void Handle_Clicked(object sender, System.EventArgs e)
        {
            this.MaterialMenu.Click();
        }

        private void Handle_MenuSelected(object sender, XF.Material.Forms.UI.MenuSelectedEventArgs e)
        {
            var selectedMenu = this.MaterialMenu.Choices[e.Result.Index];
            ViewModel.NavMenuCommand.Execute(selectedMenu);
        }

        #endregion //Event Handlers
    }
}
