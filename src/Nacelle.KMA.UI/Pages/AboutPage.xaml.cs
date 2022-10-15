using MvvmCross.Forms.Presenters.Attributes;
using Nacelle.KMA.Core.ViewModels;

namespace Nacelle.KMA.UI.Pages
{
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    public partial class AboutPage : BaseContentPage<AboutViewModel>
    {
        public AboutPage()
        {
            InitializeComponent();
        }
    }
}
