using Eventi.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventi.Domain
{
    public class Ticket
    {
        public int ID { get; set; }
        public TicketCategory TicketCategory { get; set; } // iz KupovinaTip   (=)
        public float Price { get; set; } //iz ProdajaTip.CijenaTip  (=)
        public int KupovinaTipId { get; set; }
        public PurchaseType KupovinaTip { get; set; }
        public DateTime? Purchased { get; set; }
    }
}
