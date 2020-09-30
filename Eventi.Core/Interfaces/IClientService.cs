using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventi.Core.Interfaces
{
    public interface IClientService : ICRUDService<ClientResponse, ClientSearchRequest, object, ClientUpdateRequest>
    {
        public Task<List<EventResponse>> GetEvents(int id);
    }
}
