using Nacelle.KMA.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;

namespace Nacelle.KMA.UI.Pages
{
    [MvxContentPagePresentation(WrapInNavigationPage = true, Title = "Contact Us")]
    public partial class ContactUsPage : BaseContentPage<ContactUsViewModel>
    {
        public ContactUsPage()
        {
            InitializeComponent();
        }
    }
}
