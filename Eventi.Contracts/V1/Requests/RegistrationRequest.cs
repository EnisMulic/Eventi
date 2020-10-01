namespace Eventi.Contracts.V1.Requests
{
    public abstract class RegistrationRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
