using MvvmCross.Forms.Presenters.Attributes;
using Nacelle.KMA.Core.ViewModels;

namespace Nacelle.KMA.UI.Pages
{
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    public partial class FaqMenuPage : BaseContentPage<FaqMenuViewModel>
    {
        public FaqMenuPage()
        {
            InitializeComponent();
        }
    }
}
