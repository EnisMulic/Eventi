using Eventi.Common;

namespace Eventi.Contracts.V1.Responses
{
    public class VenueResponse
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public VenueCategory VenueCategory { get; set; }
        public int CityID { get; set; }
    }
}
