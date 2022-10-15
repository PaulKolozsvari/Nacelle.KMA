using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Views
{
    public class HorizontalDashedLinesView : ContentView
    {
        private SKCanvasView _skiaCanvasView;

        public HorizontalDashedLinesView()
        {
            _skiaCanvasView = new SKCanvasView()
            {
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(5, 0),
                HeightRequest = 0.7
            };

            Content = _skiaCanvasView;
            _skiaCanvasView.PaintSurface += SkiaCanvasView_PaintSurface;
        }

        private void SkiaCanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            var info = args.Info;
            var surface = args.Surface;
            var canvas = surface.Canvas;

            canvas.Clear();

            var paint = new SKPaint
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
    }
}

