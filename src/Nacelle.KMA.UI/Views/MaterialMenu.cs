using System;
using XF.Material.Forms.UI;
using XF.Material.Forms.UI.Dialogs;
using Xamarin.Forms;
using XF.Material.Forms.Models;

namespace Nacelle.KMA.UI.Views
{
    public class MaterialMenu : MaterialMenuButton
    {
        public MaterialMenu()
        {

        }

        public void Click()
        {
            this.OnButtonClicked(false);
        }
    }
}
