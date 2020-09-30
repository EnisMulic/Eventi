namespace Eventi.Domain
{
    public class Purchase
    {
        public int ID { get; set; }
        public int ClientID { get; set; }
        public Client Client { get; set; }
        public int TicketID { get; set; }
        public Ticket Ticket { get; set; }
        public int NumberOfTickets { get; set; }
    }
}
