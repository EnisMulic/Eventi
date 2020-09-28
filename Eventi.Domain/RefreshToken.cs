using System;

namespace Eventi.Domain
{
    public class RefreshToken
    {
        public int ID { get; set; }
        public string Token { get; set; }
        public string JwtId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Used { get; set; }
        public bool Invalidated { get; set; }
        public int AccountID { get; set; }
        public Account Account { get; set; }
    }
}
