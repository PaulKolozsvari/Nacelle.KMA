using System.Collections.Generic;
using MvvmCross.Plugin.Messenger;

namespace Nacelle.KMA.Core.Messages
{
    public class SeatsSelectedMessage : MvxMessage
    {
        public SeatsSelectedMessage(object sender) : base(sender)
        {
        }

        public IDictionary<string, string> FlightIdSeatPairs { get; set; }
    }
}
