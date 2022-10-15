using Rg.Plugins.Popup.Pages;

namespace Nacelle.KMA.UI.Pages
{
    public partial class QrCodePopup : PopupPage
    {
        public QrCodePopup(string barCode)
        {
            InitializeComponent();

            qrCodeZoomView.DataContext = barCode;
        }
    }
}
