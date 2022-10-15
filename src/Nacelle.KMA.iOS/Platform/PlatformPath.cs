using System;
using Foundation;
using Nacelle.KMA.Core.Platform;
namespace Nacelle.KMA.iOS.Platform
{
    public class PlatformPath: IPlatformPath
    {
        public string Get()
        {
            return NSBundle.MainBundle.BundlePath;
        }
    }
}
