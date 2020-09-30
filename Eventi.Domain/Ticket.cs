using Eventi.Common;

namespace Eventi.Domain
{
    public class Ticket
    {
        public int ID { get; set; }
        public TicketCategory TicketCategory { get; set; } 
        public float Price { get; set; }
        public int EventID { get; set; }
        public Event Event { get; set; }
        public int SeatID { get; set; }
        public Seat Seat { get; set; }
    }
}
