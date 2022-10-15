using MvvmCross.Plugin.Messenger;

namespace Nacelle.KMA.UI.Messages
{
    public class ScrolledMessage : MvxMessage
    {
        public ScrolledMessage(object sender, double scrollX, double scrollY) : base(sender)
        {
            ScrollX = scrollX;
            ScrollY = scrollY;
        }

        public double ScrollX { get; }
        public double ScrollY { get; }
    }
}
