using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Newtonsoft.Json.Linq;
using Refit;
using System.Threading.Tasks;

namespace Eventi.Sdk
{
    public interface IAuthApi
    {
        [Post("/api/v1/Auth/Register/Client")]
        Task<ApiResponse<AuthSuccessResponse>> ClientRegisterAsync([Body] ClientRegistrationRequest registrationRequest);

        [Post("/api/v1/Auth/Register/Organizer")]
        Task<ApiResponse<AuthSuccessResponse>> OrganizerRegisterAsync([Body] ClientRegistrationRequest registrationRequest);

        [Post("/api/v1/Auth/Register/Administrator")]
        Task<ApiResponse<AuthSuccessResponse>> AdministratorRegisterAsync([Body] ClientRegistrationRequest registrationRequest);

        [Post("/api/v1/Auth/Login")]
        Task<ApiResponse<AuthSuccessResponse>> LoginAsync([Body] LoginRequest loginRequest);

        [Post("/api/v1/Auth/Refresh")]
        Task<ApiResponse<AuthSuccessResponse>> RefreshAsync([Body] RefreshTokenRequest refreshRequest);

        [Get("/api/v1/Auth/Account/{id}")]
        Task<ApiResponse<AccountResponse>> GetAsync(int id);
    }
}
