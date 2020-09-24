using System.Threading.Tasks;
using Eventi.Contracts.V1.Requests;
using Eventi.Domain;

namespace Eventi.Core.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticationResult> RegisterAsync(RegistrationRequest request);

        Task<AuthenticationResult> LoginAsync(LoginRequest request);

        Task<AuthenticationResult> RefreshTokenAsync(RefreshTokenRequest request);
    }
}
