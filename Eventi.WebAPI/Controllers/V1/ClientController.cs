using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventi.WebAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : CRUDController<ClientResponse, ClientSearchRequest, object, ClientUpdateRequest>
    {
        private readonly IClientService _service;
        public ClientController(IClientService service, IMapper mapper) : base(service, mapper)
        {
            _service = service;
        }

        [HttpGet("id/Events")]
        public async Task<ActionResult<List<EventResponse>>> GetEvents(int id)
        {
            var response = await _service.GetEvents(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}
