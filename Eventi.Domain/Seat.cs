namespace Eventi.Domain
{
    public class Seat
    {
        public int ID { get; set; }
        public int SeatNumber { get; set; } // =ProdajaTip.brojProdatihKarata
        public int TicketID { get; set; }
        public Ticket Ticket { get; set; }
    }
}
