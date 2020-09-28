namespace Eventi.Domain
{
    public class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int AccountID { get; set; }
        public Account Account { get; set; }
    }
}
