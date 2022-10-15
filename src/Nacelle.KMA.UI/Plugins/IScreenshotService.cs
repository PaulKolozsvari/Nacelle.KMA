namespace Nacelle.KMA.UI.Plugins
{
    public interface IScreenshotService
    {
        byte[] TakeViewShot(Xamarin.Forms.View view);
    }
}
