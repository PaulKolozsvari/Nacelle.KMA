using Nacelle.KMA.Core.ViewModels;
using Rg.Plugins.Popup.Pages;

namespace Nacelle.KMA.UI.Views
{
    public partial class SelectShareTravellersPopup : PopupPage
    {
        public SelectShareTravellersPopup(ICanShare viewModel)
        {
            InitializeComponent();

            selectShareTravellersView.DataContext = viewModel;
        }
    }
}
