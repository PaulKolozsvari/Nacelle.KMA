#region Using Directives

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

#endregion //Using Directives

namespace Nacelle.KMA.Core.Models.Entites
{
    public class BookingEntity
    {
        #region Constructors

        public BookingEntity()
        {
            Passengers = new List<PassengerEntity>();
        }

        #endregion //Constructors

        #region Properties

        public string ConversationID { get; set; }
        public string RecordLocator { get; set; }
        public string BookingLastName { get; set; }

        // TODO :: do we need this? It is different from PassengerSegments below
        //public List<SegmentEntity> Segments { get; set; }

        public List<PassengerEntity> Passengers { get; }

        [JsonIgnore]
        public SegmentEntity FirstSegment => PassengerSegments?.FirstOrDefault();

        [JsonIgnore]
        public SegmentEntity LastSegment => PassengerSegments?.LastOrDefault();

        [JsonIgnore]
        public List<SegmentEntity> PassengerSegments => Passengers.Any()
            ? Passengers.SelectMany(x => x.Segments).ToList()
            : null;

        [JsonIgnore]
        public bool HasFlightInTheFuture { get; set; }

        #endregion //Properties
    }
}
