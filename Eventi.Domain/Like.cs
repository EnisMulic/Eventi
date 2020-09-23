using System;

namespace Eventi.Domain
{
    public class Like
    {
        public int ClientID { get; set; }
        public Client Client { get; set; }
        public int EventID { get; set; }
        public Event Event { get; set; }
        public DateTime LikedAt { get; set; }
    }
}
