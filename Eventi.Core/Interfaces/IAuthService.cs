using System.Threading.Tasks;
using Eventi.Contracts.V1.Requests;
using Eventi.Domain;

namespace Eventi.Core.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticationResult> RegisterClientAsync(ClientRegistrationRequest request);
        Task<AuthenticationResult> RegisterAdministratorAsync(AdministratorRegistrationRequest request);
        Task<AuthenticationResult> RegisterOrganizerAsync(OrganizerRegistrationRequest request);
        Task<AuthenticationResult> LoginAsync(LoginRequest request);
        Task<AuthenticationResult> RefreshTokenAsync(RefreshTokenRequest request);
    }
}
