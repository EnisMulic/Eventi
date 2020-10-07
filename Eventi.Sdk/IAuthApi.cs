using Eventi.Contracts.V1;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Refit;
using System.Threading.Tasks;

namespace Eventi.Sdk
{
    public interface IIdentityApi
    {
        [Post(ApiRoutes.Auth.RegisterClient)]
        Task<ApiResponse<AuthSuccessResponse>> ClientRegisterAsync([Body] ClientRegistrationRequest registrationRequest);

        [Post(ApiRoutes.Auth.RegisterOrganizer)]
        Task<ApiResponse<AuthSuccessResponse>> OrganizerRegisterAsync([Body] ClientRegistrationRequest registrationRequest);

        [Post(ApiRoutes.Auth.RegisterAdministrator)]
        Task<ApiResponse<AuthSuccessResponse>> AdministratorRegisterAsync([Body] ClientRegistrationRequest registrationRequest);

        [Post(ApiRoutes.Auth.Login)]
        Task<ApiResponse<AuthSuccessResponse>> LoginAsync([Body] LoginRequest loginRequest);

        [Post(ApiRoutes.Auth.Refresh)]
        Task<ApiResponse<AuthSuccessResponse>> RefreshAsync([Body] RefreshTokenRequest refreshRequest);
    }
}
