﻿namespace Eventi.Contracts.V1.Requests
{
    public class ClientUpdateRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CreditCardNumber { get; set; }
        public string Image { get; set; }
    }
}
