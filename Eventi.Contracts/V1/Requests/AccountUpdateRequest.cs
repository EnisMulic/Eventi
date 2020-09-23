using System.ComponentModel.DataAnnotations;

namespace Eventi.Contracts.V1.Requests
{
    public class AccountUpdateRequest
    {
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
