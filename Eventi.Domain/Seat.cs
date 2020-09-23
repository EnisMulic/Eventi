using Eventi.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventi.Data.Models
{
    public class Seat
    {
        public int ID { get; set; }
        public int SeatNumber { get; set; } // =ProdajaTip.brojProdatihKarata
        public int TicketID { get; set; }
        public Ticket Ticket { get; set; }
    }
}
