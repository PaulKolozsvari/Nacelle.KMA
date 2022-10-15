using Nacelle.KMA.Core.ViewModels;
using Rg.Plugins.Popup.Pages;

namespace Nacelle.KMA.UI.Pages
{
    public partial class SelectTravellerPopup : PopupPage
    {
        public SelectTravellerPopup(SelectSeatViewModel viewModel)
        {
            InitializeComponent();

            selectTravellerView.DataContext = viewModel;
        }
    }
}
