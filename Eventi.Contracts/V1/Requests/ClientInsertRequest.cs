namespace Eventi.Contracts.V1.Requests
{
    public class ClientInsertRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string CreditCardNumber { get; set; }
        public int AccountID { get; set; }
        public string Image { get; set; }
    }
}
