using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Eventi.WebAPI.Controllers.V1
{
    public class VenueController : CRUDController<VenueResponse, VenueSearchRequest, VenueUpsertRequest, VenueUpsertRequest>
    {
        private readonly IVenueService _service;
        public VenueController(IVenueService service, IMapper mapper) : base(service, mapper)
        {
            _service = service;
        }

        [HttpGet("{id}/Event")]
        public async Task<ActionResult<PagedResponse<CityResponse>>> GetEvent(int id, [FromQuery] PaginationQuery pagination)
        {
            var response = await _service.GetEventAsync(id, pagination);
            return Ok(response);
        }
    }
}
