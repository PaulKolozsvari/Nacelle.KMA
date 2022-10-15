using Nacelle.KMA.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Pages
{
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    public partial class CheckTravellersPage : BaseContentPage<CheckTravellersViewModel>
    {
        public CheckTravellersPage()
        {
            InitializeComponent();

            NavigationPage.SetBackButtonTitle(this, "");
        }
    }
}
