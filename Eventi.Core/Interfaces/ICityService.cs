using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;

namespace Eventi.Core.Interfaces
{
    public interface ICityService : ICRUDService<CityResponse, CitySearchRequest, CityUpsertRequest, CityUpsertRequest>
    {
    }
}
