#region Using Directives

using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Plugin.Messenger;
using Nacelle.KMA.Core.ViewModels;
using Nacelle.KMA.UI.Types;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using XF.Material.Forms.UI;

#endregion //Using Directives

namespace Nacelle.KMA.UI.Pages
{
    [MvxTabbedPagePresentation(WrapInNavigationPage = false)]
    public partial class TripsPage : RefreshableContentPage<TripsViewModel>
    {
        #region Constructors

        public TripsPage()
        {
            InitializeComponent();
            base.PullToRefresh = PullToRefresh;
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            //SetupBindings();
        }

        #endregion //Constructors

        #region Methods

        protected override void OnAppearing()
        {
            base.OnAppearing();
            On<iOS>().SetUseSafeArea(true);
        }

        protected override StatusBarStyle PreferredStatusBarStyle()
        {
            return StatusBarStyle.Light;
        }

        private void Handle_Tapped(object sender, System.EventArgs e)
        {
            this.MaterialMenu.Click();
        }

        private void Handle_MenuSelected(object sender, MenuSelectedEventArgs e)
        {
            var menuItem = this.MaterialMenu.Choices[e.Result.Index];
            ViewModel.MainMenuCommand.Execute(menuItem);
        }

        #endregion //Methods
    }
}
