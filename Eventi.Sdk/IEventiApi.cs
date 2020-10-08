using Eventi.Contracts.V1;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Refit;
using System.Threading.Tasks;

namespace Eventi.Sdk
{
    [Headers("Authorization: Bearer")]
    public interface IEventiApi
    {
        [Get("/" + ApiRoutes.Country.Get)]
        Task<ApiResponse<PagedResponse<CountryResponse>>> GetCountryAsync(CountrySearchRequest request, PaginationQuery pagination);

        [Get("/" + ApiRoutes.Country.GetById)]
        Task<ApiResponse<CountryResponse>> GetCountryByIdAsync(int id);

        [Post("/" + ApiRoutes.Country.Post)]
        Task<ApiResponse<CountryResponse>> CreateCountryAsync([Body] CountryUpsertRequest request);

        [Put("/" + ApiRoutes.Country.Put)]
        Task<ApiResponse<CountryResponse>> UpdateCountryAsync(int id, [Body] CountryUpsertRequest request);

        [Delete("/" + ApiRoutes.Country.Delete)]
        Task<ApiResponse<bool>> DeleteCountryAsync(int id);
    }
}
