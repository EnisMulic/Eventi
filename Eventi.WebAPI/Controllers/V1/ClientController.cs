using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Eventi.WebAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : CRUDController<ClientResponse, ClientSearchRequest, object, ClientUpdateRequest>
    {
        public ClientController(IClientService service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
