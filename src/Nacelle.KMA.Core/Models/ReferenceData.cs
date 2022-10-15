using System;
namespace Nacelle.KMA.Core.Models
{
    public class ReferenceData
    {
        public ReferenceData()
        {
        }

        public ReferenceData(string conversationID, string bookingReference, string lastName)
        {
            ConversationID = conversationID;
            BookingReference = bookingReference;
            LastName = lastName;
        }

        public string ConversationID { get; set; }
        public string BookingReference { get; set; }
        public string LastName { get; set; }
    }
}
