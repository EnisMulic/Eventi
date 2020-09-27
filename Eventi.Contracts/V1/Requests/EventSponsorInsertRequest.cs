using Eventi.Common;

namespace Eventi.Contracts.V1.Requests
{
    public class EventSponsorInsertRequest
    {
        public int SponsorID { get; set; }
        public SponsorCategory SponsorCategory { get; set; }
    }
}
