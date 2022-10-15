using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Nacelle.KMA.Core.Commands;

namespace Nacelle.KMA.Core.Models.Items
{
    public class MenuItem: BaseMenuItem
    {
        public MenuItem(string title, string icon, Func<Task> action, string target)
        {
            Title = title;
            MenuCommand = new TrackableAsyncCommand(Constants.Analytics.Events.MenuItemTap, action, Constants.Analytics.Target.FlightCard);
            Icon = "resource://Nacelle.KMA.UI.Resources.Images." + icon;
        }

        public string Title { get; }
        public string Icon { get; }

        public ICommand MenuCommand { get; }
    }
}
