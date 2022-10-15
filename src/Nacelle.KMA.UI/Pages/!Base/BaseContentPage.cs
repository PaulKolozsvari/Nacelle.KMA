#region Using Directives

using MvvmCross;
using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using Nacelle.KMA.Core.Platform;
using Nacelle.KMA.UI.Types;
using Xamarin.Forms;

#endregion //Using Directives

namespace Nacelle.KMA.UI.Pages
{
    public class BaseContentPage<T> : MvxContentPage<T> where T : MvxViewModel
    {
        #region Constructors

        public BaseContentPage()
        {
            if (Mvx.IoCProvider.TryResolve<IStatusBarStyleManager>(out var statusBarStyleManager))
            {
                StatusBarStyleManager = statusBarStyleManager;
            }
        }

        #endregion //Constructors

        #region Fields

        protected IStatusBarStyleManager StatusBarStyleManager { get; }

        #endregion //Fields

        #region Methods

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateStatusBarStyle();
        }

        protected void SetPageStyle()
        {
            Visual = VisualMarker.Material;
        }

        protected virtual StatusBarStyle PreferredStatusBarStyle()
        {
            return StatusBarStyle.Dark;
        }

        private void UpdateStatusBarStyle()
        {
            if (PreferredStatusBarStyle() == StatusBarStyle.Dark)
            {
                StatusBarStyleManager?.SetDarkTheme();
            }
            else
            {
                StatusBarStyleManager?.SetLightTheme();
            }
        }

        #endregion //Methods
    }
}
