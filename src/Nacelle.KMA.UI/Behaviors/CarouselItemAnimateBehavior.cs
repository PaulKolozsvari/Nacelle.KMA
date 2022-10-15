using PanCardView;
using System;
using System.Collections;
using Xamarin.Forms;

namespace Nacelle.KMA.UI.Behaviors
{
    public class CarouselItemAnimateBehavior : Behavior<View>
    {
        private View _view;
        private bool _initialized;
        private object _bindingContext;

        public static readonly BindableProperty ScaleDownToProperty = BindableProperty.CreateAttached("ScaleDownTo", typeof(double), typeof(CarouselItemAnimateBehavior), Convert.ToDouble(0));

        public double ScaleDownTo
        {
            get => (double)GetValue(ScaleDownToProperty);
            set => SetValue(ScaleDownToProperty, value);
        }

        public static readonly BindableProperty RotateProperty = BindableProperty.CreateAttached("Rotate", typeof(double), typeof(CarouselItemAnimateBehavior), Convert.ToDouble(0));

        public double Rotate
        {
            get => (double)GetValue(RotateProperty);
            set => SetValue(RotateProperty, value);
        }

        protected override void OnAttachedTo(View view)
        {
            base.OnAttachedTo(view);

            _view = view;

            _view.BindingContextChanged += ViewBindingContextChanged;
            _view.PropertyChanging += ViewPropertyChanging;
        }

        protected override void OnDetachingFrom(View bindable)
        {
            base.OnDetachingFrom(bindable);

            _view.BindingContextChanged -= ViewBindingContextChanged;
            _view.PropertyChanging -= ViewPropertyChanging;

            _view = null;
            _bindingContext = null;
        }

        private void ViewBindingContextChanged(object sender, EventArgs e)
        {
            _bindingContext = _view.BindingContext;
        }

        // This is the only event I can find that will eventually yield a non-null Parent.
        private void ViewPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (_view.Parent?.Parent == null || _bindingContext == null || _initialized)
                return;

            if (_view.Parent is CoverFlowView coverFlowView)
            {
                coverFlowView.ItemAppearing += CoverFlowView_ItemAppearing;
                coverFlowView.ItemDisappearing += CoverFlowView_ItemDisappearing;

                // Make all other items smaller than the first to start off with
                if (!IsFirstItem(coverFlowView.ItemsSource))
                {
                    //_view.ScaleTo(ScaleDownTo); // This will scale X and Y axis if you prefer
                    ScaleDownYOnly();
                }

                _initialized = true;
            }
        }

        private void CoverFlowView_ItemDisappearing(CardsView view, PanCardView.EventArgs.ItemDisappearingEventArgs args)
        {
            if (_bindingContext == args.Item)
            {
                if (ScaleDownTo > 0)
                {
                    //_view.ScaleTo(ScaleDownTo); // This will scale  X and Y axis if you prefer
                    ScaleDownYOnly();
                }

                if (Rotate > 0)
                {
                    _view.RelRotateTo(Rotate, 1000);
                }
            }
        }

        private void CoverFlowView_ItemAppearing(CardsView view, PanCardView.EventArgs.ItemAppearingEventArgs args)
        {
            if (_bindingContext == args.Item)
            {
                //_view.ScaleTo(1); // This will scale in X and Y axis if you prefer
                var animation = new Animation(v => _view.ScaleY = v, ScaleDownTo, 1); // This will only do Y axis as per the Cordova App
                animation.Commit(_view, "ScaleYUp", 16, 250, Easing.CubicIn, null, () => false);
            }
        }

        private void ScaleDownYOnly()
        {
            var animation = new Animation(v => _view.ScaleY = v, 1, ScaleDownTo);
            animation.Commit(_view, "ScaleYUp", 16, 250, null, null, () => false);
        }

        private bool IsFirstItem(IEnumerable itemsSource)
        {
            object obj = null;
            foreach (var item in itemsSource)
            {
                obj = item;
                break;
            }
            var isFirst = _bindingContext == obj;
            return isFirst;
        }
    }
}
