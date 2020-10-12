using Eventi.Contracts.V1;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Refit;
using System.Threading.Tasks;

namespace Eventi.Sdk
{
    //[Headers("Authorization: Bearer")]
    public interface IEventiApi
    {
        #region Country
        [Get("/api/v1/Country")]
        Task<ApiResponse<PagedResponse<CountryResponse>>> GetCountryAsync(CountrySearchRequest request = default, PaginationQuery pagination = default);

        [Get("/api/v1/Country/{id}")]
        Task<ApiResponse<CountryResponse>> GetCountryByIdAsync(int id);

        [Post("/api/v1/Country")]
        Task<ApiResponse<CountryResponse>> CreateCountryAsync([Body] CountryUpsertRequest request);

        [Put("/api/v1/Country/{id}")]
        Task<ApiResponse<CountryResponse>> UpdateCountryAsync(int id, [Body] CountryUpsertRequest request);

        [Delete("/api/v1/Country/{id}")]
        Task <ApiResponse<bool>> DeleteCountryAsync(int id);
        #endregion

        #region Event
        [Get("/api/v1/Event")]
        Task <ApiResponse<PagedResponse<EventResponse>>> GetEventAsync(EventSearchRequest request, PaginationQuery pagination);
        #endregion

        #region Client
        [Get("/api/v1/Client")]
        Task<ApiResponse<PagedResponse<ClientResponse>>> GetClientAsync(ClientSearchRequest request = default, PaginationQuery pagination = default);

        [Delete("/api/v1/Client/{id}")]
        Task<ApiResponse<bool>> DeleteClientAsync(int id);
        #endregion

        #region Administrator
        [Get("/api/v1/Administrator")]
        Task<ApiResponse<PagedResponse<AdministratorResponse>>> GetAdministratorAsync(AdministratorSearchRequest request = default, PaginationQuery pagination = default);
        #endregion


        #region Organizer
        [Get("/api/v1/Organizer")]
        Task<ApiResponse<PagedResponse<OrganizerResponse>>> GetOrganizerAsync(OrganizerSearchRequest request = default, PaginationQuery pagination = default);
        #endregion

        #region Performer
        [Get("/api/v1/Performer")]
        Task<ApiResponse<PagedResponse<PerformerResponse>>> GetPerformerAsync(PerformerSearchRequest request = default, PaginationQuery pagination = default);

        [Get("/api/v1/Performer/{id}")]
        Task<ApiResponse<PerformerResponse>> GetPerformerAsync(int id);

        [Post("/api/v1/Performer")]
        Task<ApiResponse<PerformerResponse>> CreatePerformerAsync(PerformerUpsertRequest request);

        [Put("/api/v1/Performer/{id}")]
        Task<ApiResponse<PerformerResponse>> UpdatePerformerAsync(int id, PerformerUpsertRequest request);

        [Delete("/api/v1/Performer/{id}")]
        Task<ApiResponse<bool>> DeletePerformerAsync(int id);
        #endregion
    }
}