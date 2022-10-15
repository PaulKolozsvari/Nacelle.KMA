using MvvmCross.Forms.Presenters.Attributes;
using Nacelle.KMA.Core.ViewModels;

namespace Nacelle.KMA.UI.Pages
{
    [MvxModalPresentation(WrapInNavigationPage = true)]
    public partial class ExitRowTermsPage : BaseContentPage<ExitRowTermsViewModel>
    {
        public ExitRowTermsPage()
        {
            InitializeComponent();
        }
    }
}
