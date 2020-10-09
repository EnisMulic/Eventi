using System;
using System.Collections.Generic;
using System.Text;

namespace Eventi.Contracts.V1.Requests
{
    public class ClientSearchRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public int? AccountID { get; set; }
    }
}
