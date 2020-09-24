using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Eventi.WebAPI.Controllers.V1
{
    public class CityController : CRUDController<CityResponse, CitySearchRequest, CityUpsertRequest, CityUpsertRequest>
    {
        public CityController(ICRUDService<CityResponse, CitySearchRequest, CityUpsertRequest, CityUpsertRequest> service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
