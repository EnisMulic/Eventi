using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using System.Threading.Tasks;

namespace Eventi.Core.Interfaces
{
    public interface IClientService : ICRUDService<ClientResponse, ClientSearchRequest, object, ClientUpdateRequest>
    {
        public Task<PagedResponse<EventResponse>> GetEvents(int ID, EventSearchRequest search, PaginationQuery pagination);
    }
}
