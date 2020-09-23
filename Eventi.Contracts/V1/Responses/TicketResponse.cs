using Eventi.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventi.Contracts.V1.Responses
{
    public class TicketResponse
    {
        public int ID { get; set; }
        public TicketCategory TicketCategory { get; set; } 
        public float Price { get; set; } 
        public DateTime? Purchased { get; set; }
    }
}
