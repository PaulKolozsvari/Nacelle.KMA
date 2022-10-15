using MvvmCross.Forms.Presenters.Attributes;
using Nacelle.KMA.Core.ViewModels;
using Nacelle.KMA.UI.Pages;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace Nacelle.KMA.UI
{
    [MvxTabbedPagePresentation(TabbedPosition.Root, NoHistory = true)]
    public partial class MainPage : BaseTabbedPage<MainViewModel>
    {
        private bool _tabsLoaded;

        public MainPage()
        {
            InitializeComponent();

            On<Android>()
            .SetBarItemColor(Color.FromRgb(102, 102, 102))
            .SetBarSelectedItemColor(Color.FromRgb(140, 198, 63));


            // See https://montemagno.com/xamarin-forms-fully-customize-bottom-tabs-on-android-turn-off-shifting/

            CurrentPageChanged += MainPage_CurrentPageChanged;            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_tabsLoaded == false)
            {
                ViewModel.NavigateTabViewModelsCommand.ExecuteAsync(null);
                _tabsLoaded = true;
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();


            ViewModel.PropertyChanged += MainPage_PropertyChanged;

        }

        private void MainPage_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentTabIndex")
            {
                CurrentPage = this.Children[ViewModel.CurrentTabIndex];
            }
        }

        void MainPage_CurrentPageChanged(object sender, System.EventArgs e)
        {
            var page = CurrentPage.GetType().Name.Replace("Page", string.Empty);
            ViewModel.TabSelected(string.IsNullOrEmpty(page) ? string.Empty : page);
        }
    }
}
