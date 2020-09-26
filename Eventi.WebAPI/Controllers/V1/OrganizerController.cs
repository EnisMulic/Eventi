using AutoMapper;
using Eventi.Contracts.V1;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Eventi.WebAPI.Controllers.V1
{
    [ApiController]
    public class OrganizerController : CRUDController<OrganizerResponse, OrganizerSearchRequest, OrganizerInsertRequest, OrganizerUpdateRequest>
    {
        private readonly IOrganizerService _service;
        public OrganizerController(IOrganizerService service, IMapper mapper) : base(service, mapper)
        {
            _service = service;
        }

        [HttpGet("id/Events")]
        public async Task<ActionResult<PagedResponse<EventResponse>>> GetEvents(int id, [FromQuery] EventSearchRequest search, [FromQuery] PaginationQuery pagination)
        {
            var response = await _service.GetEvents(id, search, pagination);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}
