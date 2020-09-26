using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventi.Core.Interfaces
{
    public interface IOrganizerService : ICRUDService<OrganizerResponse, OrganizerSearchRequest, OrganizerInsertRequest, OrganizerUpdateRequest>
    {
        public Task<PagedResponse<EventResponse>> GetEvents(int ID, EventSearchRequest search, PaginationQuery pagination);
    }
}
