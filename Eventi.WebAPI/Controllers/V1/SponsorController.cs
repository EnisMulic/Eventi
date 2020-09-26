using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Eventi.WebAPI.Controllers.V1
{
    [ApiController]
    public class SponsorController : CRUDController<SponsorResponse, SponsorSearchRequest, SponsorUpsertRequest, SponsorUpsertRequest>
    {
        public SponsorController(ICRUDService<SponsorResponse, SponsorSearchRequest, SponsorUpsertRequest, SponsorUpsertRequest> service, IMapper mapper) 
            : base(service, mapper)
        {
        }
    }
}
