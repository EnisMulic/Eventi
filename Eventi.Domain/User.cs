using Microsoft.AspNetCore.Identity;

namespace Eventi.Domain
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
