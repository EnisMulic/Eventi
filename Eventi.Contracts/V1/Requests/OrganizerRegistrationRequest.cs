namespace Eventi.Contracts.V1.Requests
{
    public class OrganizerRegistrationRequest : RegistrationRequest
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int? CityID { get; set; }
    }
}
