namespace Nacelle.KMA.Core.Models.Items
{
    public class FlightStatusItem
    {
        public static string STATUS_DELAYED = "delayed";
        public static string STATUS_CANCELLED = "cancelled";

        public string Status { get; set; }

        public bool IsStatusDelayed()
        {
            return (Status == null ? string.Empty : Status.ToLowerInvariant()).Equals(STATUS_DELAYED);
        }

        public bool IsStatusCancelled()
        {
            return (Status == null ? string.Empty : Status.ToLowerInvariant()).Equals(STATUS_CANCELLED);
        }
    }
}
