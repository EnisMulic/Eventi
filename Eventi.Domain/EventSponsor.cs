using Eventi.Common;
using Eventi.Domain;

namespace Eventi.Data.Models
{
    public class EventSponsor
    {
        public int EventID { get; set; }
        public Event Event { get; set; }
        public int SponsorID { get; set; }
        public Sponsor Sponsor { get; set; }
        public SponsorCategory SponsorPriority { get; set; }
    }
}
