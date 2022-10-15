#region Using Directives

using System.Collections.Generic;
using Nacelle.KMA.API.Models.Enums;

#endregion //Using Directives

namespace Nacelle.KMA.Core.Models.Entites
{
    public class PassengerEntity
    {
        #region Constructors

        public PassengerEntity()
        {
            Segments = new List<SegmentEntity>();
        }

        #endregion //Constructors

        #region Properties

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<SegmentEntity> Segments { get; set; }
        public string WeightCategory { get; set; }
        public bool RequiresPassport { get; set; }
        public bool RequiresEmergencyContact { get; set; }
        public bool IsInhibitedBySSR { get; set; }
        public bool HasInfant { get; set; }
        public string InfantName { get; set; }
        public string InfantId { get; set; }
        public PassengerTypes PassengerType { get; set; }

        #endregion //Properties
    }
}