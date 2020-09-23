namespace Eventi.Domain
{
    public class Purchase
    {
        public int ID { get; set; }
        public int ClientID { get; set; }
        public Client Client { get; set; }
        public int EventID { get; set; }
        public Event Event { get; set; }
    }
}
