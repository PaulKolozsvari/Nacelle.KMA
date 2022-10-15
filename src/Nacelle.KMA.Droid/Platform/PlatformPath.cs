using System;
using Nacelle.KMA.Core.Platform;

namespace Nacelle.KMA.Droid.Platform
{
    public class PlatformPath: IPlatformPath
    {
        public string Get()
        {
            return "file:///android_asset/";
        }
    }
}
