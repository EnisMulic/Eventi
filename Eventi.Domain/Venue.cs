namespace Eventi.Domain
{
    public enum VenueType { Sala, Dvorana, Stadion }
    public class Venue
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public VenueType VenueType { get; set; }
        public int CityID { get; set; }
        public City City { get; set; }
    }
}
