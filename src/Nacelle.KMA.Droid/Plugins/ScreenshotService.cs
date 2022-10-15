using System.IO;
using Android.Graphics;
using Nacelle.KMA.UI.Plugins;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Nacelle.KMA.Droid.Plugins
{
    public class ScreenshotService : IScreenshotService
    {
        public byte[] TakeViewShot(View view)
        {
            var nativeView = view.GetRenderer().View;
            var wasDrawingCacheEnabled = nativeView.DrawingCacheEnabled;
            nativeView.DrawingCacheEnabled = true;
            nativeView.BuildDrawingCache(false);
            var bitmap = nativeView.GetDrawingCache(false);            

            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 90, stream);

                nativeView.DrawingCacheEnabled = wasDrawingCacheEnabled;
                return stream.ToArray();
            }
        }
    }
}
