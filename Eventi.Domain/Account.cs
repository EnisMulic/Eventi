using Eventi.Common;

namespace Eventi.Domain
{
    public class Account
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public AccountCategory? AccountCategory { get; set; }
    }
}
