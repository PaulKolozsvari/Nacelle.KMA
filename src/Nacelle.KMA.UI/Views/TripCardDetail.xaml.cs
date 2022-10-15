#region Using Directives

using System;
using System.Diagnostics;
using System.Windows.Input;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

#endregion //Using Directives

namespace Nacelle.KMA.UI.Views
{
    public partial class TripCardDetail : ContentView
    {
        #region Constructors

        public TripCardDetail()
        {
            InitializeComponent();
        }

        #endregion //Constructors

        #region Properties

        #region Caption

        public static BindableProperty CaptionProperty = BindableProperty.Create(nameof(Caption), typeof(string), typeof(TripCardDetail), string.Empty, BindingMode.TwoWay);

        public string Caption
        {
            get
            {
                string result = (string)base.GetValue(CaptionProperty);
                return result;
            }
            set
            {
                base.SetValue(CaptionProperty, value);
            }
        }

        #endregion //Caption

        #region Number

        public static BindableProperty NumberProperty = BindableProperty.Create(nameof(Number), typeof(string), typeof(TripCardDetail), string.Empty, BindingMode.TwoWay);

        public string Number
        {
            get
            {
                string result = (string)base.GetValue(NumberProperty);
                return result;
            }
            set => base.SetValue(NumberProperty, value);
        }

        #endregion //Number

        #region Flight Date Time

        public static BindableProperty FlightDateTimeProperty = BindableProperty.Create(nameof(FlightDateTime), typeof(string), typeof(TripCardDetail), string.Empty, BindingMode.TwoWay);

        public string FlightDateTime
        {
            get
            {
                string result = (string)base.GetValue(FlightDateTimeProperty);
                return result;
            }
            set
            {
                base.SetValue(FlightDateTimeProperty, value);
            }
        }

        #endregion //Flight Date Time

        #region Check-In Button Visibility

        public static BindableProperty CheckInButtonIsVisibleProperty = BindableProperty.Create(nameof(CheckInButtonIsVisible), typeof(bool), typeof(TripCardDetail), false, BindingMode.TwoWay, propertyChanged: ButtonStackVisibilityChanged);

        public bool CheckInButtonIsVisible
        {
            get
            {
                var result = (bool)base.GetValue(CheckInButtonIsVisibleProperty);
                return result;
            }
            set
            {
                base.SetValue(CheckInButtonIsVisibleProperty, value);
            }
        }

        #endregion //Check-In Button Visibility

        #region View Boarding Pass Button Visibility

        public static BindableProperty ViewBoardingPassButtonIsVisibleProperty = BindableProperty.Create(nameof(ViewBoardingPassButtonIsVisible), typeof(bool), typeof(TripCardDetail), false, BindingMode.TwoWay, propertyChanged: ButtonStackVisibilityChanged);

        public bool ViewBoardingPassButtonIsVisible
        {
            get
            {
                bool result = (bool)base.GetValue(ViewBoardingPassButtonIsVisibleProperty);
                return result;
            }
            set
            {
                base.SetValue(ViewBoardingPassButtonIsVisibleProperty, value);
            }
        }

        #endregion //View Boarding Pass Button Visibility

        #endregion //Properties

        #region Commands

        #region Check-in Command

        public static readonly BindableProperty CheckInCommandProperty = BindableProperty.Create(nameof(CheckInCommand), typeof(ICommand), typeof(TripCardDetail));

        public ICommand CheckInCommand
        {
            get => (ICommand)GetValue(CheckInCommandProperty);
            set => SetValue(CheckInCommandProperty, value);
        }

        #endregion //Check-in Command

        #endregion //Commands

        #region View Boarding Pass Command

        public static readonly BindableProperty ViewBoardingPassCommandProperty = BindableProperty.Create(nameof(ViewBoardingPassCommand), typeof(ICommand), typeof(TripCardDetail));

        public ICommand ViewBoardingPassCommand
        {
            get => (ICommand)GetValue(ViewBoardingPassCommandProperty);
            set => SetValue(ViewBoardingPassCommandProperty, value);
        }

        #endregion //View Boarding Pass Command

        #region Flight Details Command

        public static readonly BindableProperty FlightDetailCommandProperty = BindableProperty.Create(nameof(FlightDetailCommand), typeof(ICommand), typeof(TripCardDetail));

        public ICommand FlightDetailCommand
        {
            get => (ICommand)GetValue(FlightDetailCommandProperty);
            set => SetValue(FlightDetailCommandProperty, value);
        }

        private static void ButtonStackVisibilityChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is TripCardDetail tripCardDetail)
            {
                tripCardDetail.ButtonStack.IsVisible = tripCardDetail.CheckInButtonIsVisible || tripCardDetail.ViewBoardingPassButtonIsVisible;
            }
        }

        #endregion //Flight Details Command

        // ViewBoardingPassCommand

        #region Event Handlers

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
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

        #endregion //Event Handlers
    }
}
