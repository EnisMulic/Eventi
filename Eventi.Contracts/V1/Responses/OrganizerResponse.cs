﻿namespace Eventi.Contracts.V1.Responses
{
    public class OrganizerResponse
    {
        public int ID { get; set; }
        public int AccountID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int? CityID { get; set; }
    }
}
