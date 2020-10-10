using Eventi.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventi.Contracts.V1.Responses
{
    public class AccountResponse
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public AccountCategory AccountCategory { get; set; }
    }
}
