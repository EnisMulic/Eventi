﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Eventi.Contracts.V1.Responses
{
    public class ClientResponse
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }
    }
}
