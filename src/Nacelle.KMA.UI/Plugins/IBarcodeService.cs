using System.IO;

namespace Nacelle.KMA.UI.Plugins
{
    public interface IBarcodeService
    {
        Stream CreateBarcode(string qrText, int width, int height);
    }
}
