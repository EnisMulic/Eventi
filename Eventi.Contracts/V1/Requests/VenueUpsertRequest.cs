using Eventi.Common;

namespace Eventi.Contracts.V1.Requests
{
    public class VenueUpsertRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public VenueCategory VenueCategory { get; set; }
        public int CityID { get; set; }
    }
}
