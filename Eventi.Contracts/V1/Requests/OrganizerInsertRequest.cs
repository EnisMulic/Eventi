namespace Eventi.Contracts.V1.Requests
{
    public class OrganizerInsertRequest
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int? CityID { get; set; }
        public int? AccountID { get; set; }
    }
}
