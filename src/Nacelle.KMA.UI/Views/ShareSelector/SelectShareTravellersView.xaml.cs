using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MvvmCross;
using MvvmCross.Forms.Views;
using Nacelle.KMA.Core.Models.Items;
using Nacelle.KMA.Core.Platform;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Views
{
    public partial class SelectShareTravellersView : MvxContentView
    {
        public static readonly BindableProperty ShareCommandProperty = BindableProperty.Create(
            nameof(ShareCommand),
            typeof(ICommand),
            typeof(SelectShareTravellersView));

        public ICommand ShareCommand
        {
            get => (ICommand)GetValue(ShareCommandProperty);
            set => SetValue(ShareCommandProperty, value);
        }

        public static readonly BindableProperty TravellerItemsProperty = BindableProperty.Create(
            nameof(TravellerItems),
            typeof(List<TravellerItem>),
            typeof(SelectShareTravellersView));

        public List<TravellerItem> TravellerItems
        {
            get => (List<TravellerItem>)GetValue(TravellerItemsProperty);
            set => SetValue(TravellerItemsProperty, value);
        }

        public SelectShareTravellersView()
        {
            InitializeComponent();
        }

        private async void CancelClicked(object sender, System.EventArgs e)
        {
            if (Parent is SelectShareTravellersPopup popup)
            {
                await PopupNavigation.Instance.RemovePageAsync(popup);
            }
        }

        private async void ShareClicked(object sender, System.EventArgs e)
        {
            var travellersToShare = TravellerItems.Where(x => x.DoShare).ToList();
            if (travellersToShare.Any())
            {
                if (ShareCommand != null && ShareCommand.CanExecute(travellersToShare))
                {
                    await PopupNavigation.Instance.RemovePageAsync((SelectShareTravellersPopup)Parent);
                    ShareCommand.Execute(travellersToShare);
                }
            }
            else
            {
                var alertService = Mvx.IoCProvider.Resolve<IAlertService>();
                alertService.Show("", "Please select at least one passenger", (Title: "OK", null));
            }
        }
    }
}
