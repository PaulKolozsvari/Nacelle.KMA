using System.IO;
using Android.Graphics;
using Nacelle.KMA.UI.Plugins;

namespace Nacelle.KMA.Droid.Plugins
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
                    Height = height,
                    Margin = 10
                }
            };

            barcodeWriter.Renderer = new ZXing.Mobile.BitmapRenderer();
            var bitmap = barcodeWriter.Write(qrText);
            var stream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);
            stream.Position = 0;
            return stream;
        }
    }
}
