namespace Eventi.Contracts.V1.Requests
{
    public class AdministratorRegistrationRequest : RegistrationRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
