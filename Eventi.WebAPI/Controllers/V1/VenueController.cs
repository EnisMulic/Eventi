using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Eventi.WebAPI.Controllers.V1
{
    public class VenueController : CRUDController<VenueResponse, VenueSearchRequest, VenueUpsertRequest, VenueUpsertRequest>
    {
        public VenueController(ICRUDService<VenueResponse, VenueSearchRequest, VenueUpsertRequest, VenueUpsertRequest> service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
