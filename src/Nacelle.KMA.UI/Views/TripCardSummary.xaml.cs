#region Using Directives

using System.Windows.Input;
using Xamarin.Forms;
using Nacelle.KMA.Core.Models.Items;
using System.Collections.Generic;
using System;

#endregion //Using Directives

namespace Nacelle.KMA.UI.Views
{
    public partial class TripCardSummary : ContentView
    {
        #region Constructors

        public TripCardSummary()
        {
            InitializeComponent();
        }

        #endregion //Constructors

        // Expanded

        #region Expanded

        public static BindableProperty ExpandedProperty = BindableProperty.Create(nameof(Expanded), typeof(bool), typeof(TripCardSummary), false, BindingMode.TwoWay);

        public bool Expanded
        {
            get => (bool)base.GetValue(ExpandedProperty);
            set => base.SetValue(ExpandedProperty, value);
        }

        #endregion //Expanded

        #region Day

        public static BindableProperty DayProperty = BindableProperty.Create(nameof(Day), typeof(string), typeof(TripCardSummary), string.Empty, BindingMode.TwoWay);

        public string Day
        {
            get => (string)base.GetValue(DayProperty);
            set => base.SetValue(DayProperty, value);
        }

        #endregion //Day

        #region Month

        public static BindableProperty MonthProperty = BindableProperty.Create(nameof(Month), typeof(string), typeof(TripCardSummary), string.Empty, BindingMode.TwoWay);

        public string Month
        {
            get => (string)base.GetValue(MonthProperty);
            set => base.SetValue(MonthProperty, value);
        }

        #endregion //Month

        #region Year

        public static BindableProperty YearProperty = BindableProperty.Create(nameof(Year), typeof(string), typeof(TripCardSummary), string.Empty, BindingMode.TwoWay);

        public string Year
        {
            get => (string)base.GetValue(YearProperty);
            set => base.SetValue(YearProperty, value);
        }

        #endregion //Month

        #region Caption

        public static BindableProperty CaptionProperty = BindableProperty.Create(nameof(Caption), typeof(string), typeof(TripCardSummary), string.Empty, BindingMode.TwoWay);

        public string Caption
        {
            get => (string)base.GetValue(CaptionProperty);
            set => base.SetValue(CaptionProperty, value);
        }

        #endregion //Caption

        #region Booking Reference

        public static BindableProperty BookingReferenceProperty = BindableProperty.Create(nameof(BookingReference), typeof(string), typeof(TripCardSummary), string.Empty, BindingMode.TwoWay);

        public string BookingReference
        {
            get => (string)base.GetValue(BookingReferenceProperty);
            set => base.SetValue(BookingReferenceProperty, value);
        }

        #endregion //Booking Reference

        // ToggleExpandedCommand

        #region Toggle Expanded Command

        public static readonly BindableProperty ToggleExpandedCommandProperty = BindableProperty.Create(nameof(ToggleExpandedCommand), typeof(ICommand), typeof(TripCardSummary));

        public ICommand ToggleExpandedCommand
        {
            get => (ICommand)GetValue(ToggleExpandedCommandProperty);
            set => SetValue(ToggleExpandedCommandProperty, value);
        }

        #endregion //Toggle Expanded Command

        #region Trip Item Menu Command

        public static readonly BindableProperty TripItemMenuCommandProperty = BindableProperty.Create(nameof(TripItemMenuCommand), typeof(ICommand), typeof(TripCardSummary));

        public ICommand TripItemMenuCommand
        {
            get => (ICommand)GetValue(TripItemMenuCommandProperty);
            set => SetValue(TripItemMenuCommandProperty, value);
        }

        #endregion //Trip Item Menu Command

        #region Trip Menu Items

        public static readonly BindableProperty TripMenuItemsProperty = BindableProperty.Create(nameof(TripMenuItems), typeof(IList<string>), typeof(TripCardSummary), propertyChanged: OnTripsMenuItemsChanged);

        private static void OnTripsMenuItemsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            //((TripCardSummary)bindable).MaterialMenu.Choices = newValue as string[];
        }

        public IList<string> TripMenuItems
        {
            get => (IList<string>)GetValue(TripMenuItemsProperty);
            set => SetValue(TripMenuItemsProperty, value);
        }

        #endregion //Trip Menu Items

        #region Event Handlers

        private void Handle_MenuSelected(object sender, XF.Material.Forms.UI.MenuSelectedEventArgs e)
        {
            var tripItem = BindingContext as TripItem;
            var selectedMenu = this.MaterialMenu.Choices[e.Result.Index];
            if (tripItem == null)
            {
                tripItem = new TripItem();
            }
            tripItem.SelectedMenu = selectedMenu.ToString();
            TripItemMenuCommand.Execute(tripItem);
        }

        #endregion //Event Handlers
    }
}
