using Eventi.Common;

namespace Eventi.Domain
{
    public class SaleType
    {
        public int ID { get; set; }
        public TicketCategory TicketCategory { get; set; }
        public int NumberOfTickets { get; set; }
        public float Price { get; set; }
        public bool SeatsExist { get; set; }
        public int EventID { get; set; }
        public Event Event { get; set; }
    }
}
