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
        [Get("/api/v1/Country")]
        Task<ApiResponse<PagedResponse<CountryResponse>>> GetCountryAsync(CountrySearchRequest request, PaginationQuery pagination);

        [Get("/api/v1/Country/{id}")]
        Task<ApiResponse<CountryResponse>> GetCountryByIdAsync(int id);

        [Post("/api/v1/Country")]
        Task<ApiResponse<CountryResponse>> CreateCountryAsync([Body] CountryUpsertRequest request);

        [Put("/api/v1/Country/{id}")]
        Task<ApiResponse<CountryResponse>> UpdateCountryAsync(int id, [Body] CountryUpsertRequest request);

        [Delete("/api/v1/Country/{id}")]
        Task <ApiResponse<bool>> DeleteCountryAsync(int id);

        [Refit.Get("/api/v1/Event")]
        Task <ApiResponse<PagedResponse<EventResponse>>> GetEventAsync(EventSearchRequest request, PaginationQuery pagination);
    }
}
