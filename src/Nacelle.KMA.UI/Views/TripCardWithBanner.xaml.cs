using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Windows.Input;
using SkiaSharp;

namespace Nacelle.KMA.UI.Views
{
    public partial class TripCardWithBanner : ContentView
    {
        // HeaderImage

        public static BindableProperty HeaderImageProperty = BindableProperty.Create(
            nameof(HeaderImage),
            typeof(ImageSource),
            typeof(TripCardWithBanner),
            null,
            BindingMode.TwoWay);

        public ImageSource HeaderImage
        {
            get => (ImageSource)GetValue(HeaderImageProperty);
            set => SetValue(HeaderImageProperty, value);
        }

        // To

        public static BindableProperty ToProperty = BindableProperty.Create(
            nameof(To),
            typeof(string),
            typeof(TripCardWithBanner),
            string.Empty,
            BindingMode.TwoWay);

        public string To
        {
            get => (string)GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }

        // From

        public static BindableProperty FromProperty = BindableProperty.Create(
            nameof(From),
            typeof(string),
            typeof(TripCardWithBanner),
            string.Empty,
            BindingMode.TwoWay);

        public string From
        {
            get => (string)GetValue(FromProperty);
            set => SetValue(FromProperty, value);
        }

        // Caption

        public static BindableProperty CaptionProperty = BindableProperty.Create(
            nameof(Caption),
            typeof(string),
            typeof(TripCardWithBanner),
            string.Empty,
            BindingMode.TwoWay);

        public string Caption
        {
            get => (string)GetValue(CaptionProperty);
            set => SetValue(CaptionProperty, value);
        }

        // FlightNumber

        public static BindableProperty FlightNumberProperty = BindableProperty.Create(
            nameof(FlightNumber),
            typeof(string),
            typeof(TripCardWithBanner),
            string.Empty,
            BindingMode.TwoWay);

        public string FlightNumber
        {
            get => (string)GetValue(FlightNumberProperty);
            set => SetValue(FlightNumberProperty, value);
        }

        // FlightDateTime

        public static BindableProperty FlightDateTimeProperty = BindableProperty.Create(
            nameof(FlightDateTime),
            typeof(string),
            typeof(TripCardWithBanner),
            string.Empty,
            BindingMode.TwoWay);

        public string FlightDateTime
        {
            get => (string)GetValue(FlightDateTimeProperty);
            set => SetValue(FlightDateTimeProperty, value);
        }

        // IsViewBoardingPassVisible

        public static BindableProperty IsViewBoardingPassButtonVisibleProperty = BindableProperty.Create(
            nameof(IsViewBoardingPassButtonVisible),
            typeof(bool),
            typeof(TripCardWithBanner),
            false,
            BindingMode.TwoWay);

        public bool IsViewBoardingPassButtonVisible
        {
            get => (bool)GetValue(IsViewBoardingPassButtonVisibleProperty);
            set => SetValue(IsViewBoardingPassButtonVisibleProperty, value);
        }

        // IsCheckInButtonVisible

        public static BindableProperty IsCheckInButtonVisibleProperty = BindableProperty.Create(
            nameof(IsCheckInButtonVisible),
            typeof(bool),
            typeof(TripCardWithBanner),
            false,
            BindingMode.TwoWay);

        public bool IsCheckInButtonVisible
        {
            get => (bool)GetValue(IsCheckInButtonVisibleProperty);
            set => SetValue(IsCheckInButtonVisibleProperty, value);
        }

        // FlightDetailsCommand

        public static BindableProperty FlightDetailsCommandProperty = BindableProperty.Create(
            nameof(FlightDetailsCommand),
            typeof(ICommand),
            typeof(TripCardWithBanner),
            null,
            BindingMode.TwoWay);

        public ICommand FlightDetailsCommand
        {
            get => (ICommand)GetValue(FlightDetailsCommandProperty);
            set => SetValue(FlightDetailsCommandProperty, value);
        }

        // CheckInCommand

        public static BindableProperty CheckInCommandProperty = BindableProperty.Create(
            nameof(CheckInCommand),
            typeof(ICommand),
            typeof(TripCardWithBanner),
            null,
            BindingMode.TwoWay);

        public ICommand CheckInCommand
        {
            get => (ICommand)GetValue(CheckInCommandProperty);
            set => SetValue(CheckInCommandProperty, value);
        }

        // ViewBoardingPassCommand

        public static BindableProperty ViewBoardingPassCommandProperty = BindableProperty.Create(
            nameof(ViewBoardingPassCommand),
            typeof(ICommand),
            typeof(TripCardWithBanner),
            null,
            BindingMode.TwoWay);

        public ICommand ViewBoardingPassCommand
        {
            get => (ICommand)GetValue(ViewBoardingPassCommandProperty);
            set => SetValue(ViewBoardingPassCommandProperty, value);
        }

        public TripCardWithBanner()
        {
            InitializeComponent();
        }

        private void OnCanvasViewPaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs args)
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
