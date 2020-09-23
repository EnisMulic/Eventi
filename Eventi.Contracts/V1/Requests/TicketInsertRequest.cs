using Eventi.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventi.Contracts.V1.Requests
{
    public class TicketInsertRequest
    {
        public TicketCategory TicketCategory { get; set; }
        public float Price { get; set; }
    }
}
