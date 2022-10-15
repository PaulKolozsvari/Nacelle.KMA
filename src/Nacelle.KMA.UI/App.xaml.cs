using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Nacelle.KMA.UI
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);
        }

        protected override void OnStart()
        {
            base.OnStart();

            AppCenter.Start("ios=455cd71c-8b5c-4f04-8c55-7dfd7546f15f;android=c5ad005d-bce9-4dc9-84aa-ca4abf327655", typeof(Crashes));
        }
    }
}
