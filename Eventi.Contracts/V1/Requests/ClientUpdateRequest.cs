using System;
using System.Collections.Generic;
using System.Text;

namespace Eventi.Contracts.V1.Requests
{
    public class ClientUpdateRequest
    {
        public string Address { get; set; }
        public string CreditCardNumber { get; set; }
        public int PersonID { get; set; }
        public string Image { get; set; }
    }
}
