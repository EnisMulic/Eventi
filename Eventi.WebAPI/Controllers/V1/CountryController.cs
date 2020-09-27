using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventi.WebAPI.Controllers.V1
{
    public class CountryController : CRUDController<CountryResponse, CountrySearchRequest, CountryUpsertRequest, CountryUpsertRequest>
    {
        private readonly ICountryService _service;
        public CountryController(ICountryService service, IMapper mapper) : base(service, mapper)
        {
            _service = service;
        }

        [HttpGet("{id}/City")]
        public async Task<ActionResult<List<CityResponse>>> GetCity(int id)
        {
            var response = await _service.GetCityAsync(id);
            return Ok(response);
        }
    }
}
