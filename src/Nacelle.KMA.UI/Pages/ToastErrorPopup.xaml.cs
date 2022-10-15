#region Using Directives

using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;

#endregion //Using Directives

using Xamarin.Forms;

namespace Nacelle.KMA.UI.Pages
{
    public partial class ToastErrorPopup : PopupPage
    {
        #region Constructors

        public ToastErrorPopup(string message)
        {
            InitializeComponent();
            Animation = new Rg.Plugins.Popup.Animations.ScaleAnimation();
            lblMessage.Text = message;
        }

        #endregion //Constructors
    }
}
