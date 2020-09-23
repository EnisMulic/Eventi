using Eventi.Common;

namespace Eventi.Domain
{
    public class Venue
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public VenueCategory VenueCategory { get; set; }
        public int CityID { get; set; }
        public City City { get; set; }
    }
}
