using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using System.Threading.Tasks;

namespace Eventi.Core.Interfaces
{
    public interface IVenueService : ICRUDService<VenueResponse, VenueSearchRequest, VenueUpsertRequest, VenueUpsertRequest>
    {
        public Task<PagedResponse<EventResponse>> GetEventAsync(int id, PaginationQuery pagination);
    }
}
