namespace Nacelle.KMA.Core.Models.Items
{
    public class FlightExtraItem
    {
        public FlightExtraItem(string title, string description, string icon)
        {
            Title = title;
            Icon = "resource://Nacelle.KMA.UI.Resources.Images." + icon;
            Description = description;
        }

        public string Title { get; }
        public string Icon { get; }
        public string Description { get; }
    }
}
