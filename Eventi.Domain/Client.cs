namespace Eventi.Domain
{
    public class Client
    {
        public int ID { get; set; }
        public string Address { get; set; }
        public string CreditCardNumber { get; set; }
        public int PersonID { get; set; }
        public Person Person { get; set; }
        public string Image { get; set; }
    }
}
