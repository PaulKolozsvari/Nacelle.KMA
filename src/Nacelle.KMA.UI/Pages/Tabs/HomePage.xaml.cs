#region Using Directives

using MvvmCross.Forms.Presenters.Attributes;
using Nacelle.KMA.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Nacelle.KMA.UI.Types;
using Refractored.XamForms.PullToRefresh;
using Xamarin.Essentials;
using MvvmCross.Plugin.Messenger;
using MvvmCross;
using SkiaSharp;

#endregion //Using Directives

namespace Nacelle.KMA.UI.Pages
{
    [MvxTabbedPagePresentation(WrapInNavigationPage = false)]
    public partial class HomePage : RefreshableContentPage<HomeViewModel>
    {
        #region Constructors

        public HomePage()
        {
            InitializeComponent();
            base.PullToRefresh = PullToRefresh;
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
        }

        #endregion //Constructors

        #region Fields

        protected override StatusBarStyle PreferredStatusBarStyle() => StatusBarStyle.Light;

        #endregion //Fields

        #region Methods

        protected override void OnAppearing()
        {
            base.OnAppearing();
            On<iOS>().SetUseSafeArea(true);
        }

        private async void RefreshLabel_Tapped(object sender, System.EventArgs e)
        {
            await RefreshLabel.RelRotateTo(180, 500);
        }

        private void OnCanvasViewPaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs args)
        {
            var info = args.Info;
            var surface = args.Surface;
            var canvas = surface.Canvas;

            canvas.Clear();

            var paint = new SkiaSharp.SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Gray,
                StrokeWidth = 15,
                StrokeCap = SKStrokeCap.Butt,
                PathEffect = SKPathEffect.CreateDash(new float[] { 15, 10 }, 10)
            };

            var path = new SKPath();
            path.LineTo(info.Width, 0);
            canvas.DrawPath(path, paint);
        }

        #endregion //Methods
    }
}
