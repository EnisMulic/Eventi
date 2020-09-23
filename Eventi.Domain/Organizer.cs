namespace Eventi.Domain
{
    public class Organizer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int? CityID { get; set; }
        public City City { get; set; }
        public int? AccountID { get; set; }
        public Account Account { get; set; }
    }
}
