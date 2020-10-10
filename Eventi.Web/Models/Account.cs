using Eventi.Common;

namespace Eventi.Web.Models
{
    public class Account
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public AccountCategory AccountCategory { get; set; }
    }
}
