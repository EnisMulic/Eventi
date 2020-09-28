using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventi.Core.Interfaces
{
    public interface ICityService : ICRUDService<CityResponse, CitySearchRequest, CityUpsertRequest, CityUpsertRequest>
    {
        public Task<PagedResponse<VenueResponse>> GetVenueAsync(int id, PaginationQuery pagination);
        public Task<PagedResponse<EventResponse>> GetEventAsync(int id, PaginationQuery pagination);
    }
}
