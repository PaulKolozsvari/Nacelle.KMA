using System.ComponentModel;

namespace Nacelle.KMA.Core.Models.Enums
{
    public enum WeightCategory
    {
        [Description("UNKNOWN")]
        Unknown = -1,

        [Description("ADULT_MALE")]
        AdultMale = 0,

        [Description("ADULT_FEMALE")]
        AdultFemale = 1,

        [Description("CHILD")]
        Child = 2
    }
}
