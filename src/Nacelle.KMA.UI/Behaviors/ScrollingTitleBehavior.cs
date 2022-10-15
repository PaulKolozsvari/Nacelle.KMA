using System;
using Nacelle.KMA.UI.Views;
using Xamarin.Forms;
using System.Diagnostics;

namespace Nacelle.KMA.UI.Behaviors
{
    public class ScrollingTitleBehavior: Behavior<ScrollView>
    {
        public static readonly BindableProperty CustomNavigationBarProperty = BindableProperty.CreateAttached("CustomNavigationBar", typeof(CustomNavigationBar), typeof(ScrollingTitleBehavior), null);

        public CustomNavigationBar CustomNavigationBar
        {
            get => (CustomNavigationBar)GetValue(CustomNavigationBarProperty);
            set => SetValue(CustomNavigationBarProperty, value);
        }

        protected override void OnAttachedTo(ScrollView bindable)
        {
            base.OnAttachedTo(bindable);

            if (CustomNavigationBar != null)
            {
                CustomNavigationBar.Label.IsVisible = false;
            }

            bindable.Scrolled += ScrollView_Scrolled;
        }

        protected override void OnDetachingFrom(ScrollView bindable)
        {
            bindable.Scrolled -= ScrollView_Scrolled;
            base.OnDetachingFrom(bindable);
        }

        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            if (CustomNavigationBar == null)
            {
                return;
            }

            CustomNavigationBar.Label.IsVisible = e.ScrollY > 50;
        }
    }
}
