using System.Threading.Tasks;
using Eventi.Contracts.V1.Requests;
using Eventi.Domain;

namespace Eventi.Core.Interfaces
{
    public interface IUserAccountService
    {
        Task<AuthenticationResult> RegisterAsync(UserAccountRegistrationRequest request);

        Task<AuthenticationResult> AuthenticateAsync(UserAccountAuthenticationRequest request);

        Task<AuthenticationResult> RefreshTokenAsync(RefreshTokenRequest request);
    }
}
