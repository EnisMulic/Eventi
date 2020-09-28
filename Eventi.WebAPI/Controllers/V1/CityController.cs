using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Eventi.WebAPI.Controllers.V1
{
    public class CityController : CRUDController<CityResponse, CitySearchRequest, CityUpsertRequest, CityUpsertRequest>
    {
        private readonly ICityService _service;
        public CityController(ICityService service, IMapper mapper) : base(service, mapper)
        {
            _service = service;
        }

        [HttpGet("{id}/Venue")]
        public async Task<ActionResult<PagedResponse<CityResponse>>> GetVenue(int id, [FromQuery] PaginationQuery pagination)
        {
            var response = await _service.GetVenueAsync(id, pagination);
            return Ok(response);
        }

        [HttpGet("{id}/Event")]
        public async Task<ActionResult<PagedResponse<CityResponse>>> GetEvent(int id, [FromQuery] PaginationQuery pagination)
        {
            var response = await _service.GetEventAsync(id, pagination);
            return Ok(response);
        }
    }
}
