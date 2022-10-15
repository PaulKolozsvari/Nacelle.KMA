using System.IO;
using Nacelle.KMA.UI.Plugins;

namespace Nacelle.KMA.iOS.Plugins
{
    public class BarcodeService : IBarcodeService
    {
        public Stream CreateBarcode(string qrText, int width, int height)
        {
            var barcodeWriter = new ZXing.Mobile.BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = width,
                    Height = height
                }
            };

            barcodeWriter.Renderer = new ZXing.Mobile.BitmapRenderer();
            var image = barcodeWriter.Write(qrText);

            return image.AsPNG().AsStream();
        }
    }
}
