using System;
using System.Runtime.InteropServices;
using UIKit;
using Xamarin.Forms;

namespace Nacelle.KMA.iOS.Plugins
{
    public class ScreenshotService : UI.Plugins.IScreenshotService
    {  
        public byte[] TakeViewShot(View view)
        {
            var nativeView = Xamarin.Forms.Platform.iOS.Platform.GetRenderer(view).NativeView;
            UIGraphics.BeginImageContextWithOptions(nativeView.Bounds.Size, opaque: true, scale: 0);
            nativeView.DrawViewHierarchy(nativeView.Bounds, afterScreenUpdates: true);
            var image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            using (var data = image.AsPNG())
            {
                var bytes = new byte[data.Length];
                Marshal.Copy(data.Bytes, bytes, 0, Convert.ToInt32(data.Length));

                return bytes;
            }
        }
    }
}
