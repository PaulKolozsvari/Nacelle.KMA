#region Using Directives

using System;
using System.Threading.Tasks;
using Nacelle.KMA.Core.Platform;
using Nacelle.KMA.UI.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

#endregion //Using Directives

namespace Nacelle.KMA.UI.Services
{
    public class ToastService: IToastService
    {
        #region Methods

        public async Task Show(string message, bool isError = false)
        {
            if (isError)
            {
                await PopupNavigation.Instance.PushAsync(new ToastErrorPopup(message));
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new ToastInfoPopup(message));
            }
            await Task.Delay(5000);
            await PopupNavigation.PopAllAsync();
        }

        #endregion //Methods
    }
}
