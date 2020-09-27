using Eventi.Common;

namespace Eventi.Domain
{
    public class EventSponsor
    {
        public int EventID { get; set; }
        public Event Event { get; set; }
        public int SponsorID { get; set; }
        public Sponsor Sponsor { get; set; }
        public SponsorCategory SponsorCategory { get; set; }
    }
}
