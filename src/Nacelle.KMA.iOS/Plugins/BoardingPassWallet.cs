using System.Linq;
using System.Threading.Tasks;
using Foundation;
using Nacelle.KMA.Core.Platform;
using PassKit;
using UIKit;

namespace Nacelle.KMA.iOS.Plugins
{
    public class BoardingPassWallet : PKAddPassesViewControllerDelegate, IPassKitService
    {
        public async Task AddToWallet(byte[] passJson)
        {
            if (PKPassLibrary.IsAvailable)
            {
                var pass = new PKPass(NSData.FromArray(passJson), out NSError error);

                if (error == null)
                {
                    var pkapvc = new PKAddPassesViewController(pass);

                    var topVC = GetTopViewController();
                    await topVC.PresentViewControllerAsync(pkapvc, true);
                }
            }
        }

        private static UIViewController GetTopViewController()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController != null)
                vc = vc.PresentedViewController;

            if (vc is UINavigationController navController)
                vc = navController.ViewControllers.Last();

            return vc;
        }
    }
}
