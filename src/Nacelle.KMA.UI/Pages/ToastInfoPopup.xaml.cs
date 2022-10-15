#region Using Directives

using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

#endregion //Using Directives

namespace Nacelle.KMA.UI.Pages
{
    public partial class ToastInfoPopup : PopupPage
    {
        #region Constructors

        public ToastInfoPopup(string message)
        {
            InitializeComponent();
            Animation = new Rg.Plugins.Popup.Animations.ScaleAnimation();
            lblMessage.Text = message;
        }
            
        #endregion //Constructors
    }
}
