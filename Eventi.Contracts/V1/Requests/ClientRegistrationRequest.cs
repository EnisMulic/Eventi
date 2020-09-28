namespace Eventi.Contracts.V1.Requests
{
    public class ClientRegistrationRequest : RegistrationRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string CreditCardNumber { get; set; }
    }
}
