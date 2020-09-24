using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Eventi.WebAPI.Controllers.V1
{
    [ApiController]
    public class PerformerController : CRUDController<PerformerResponse, PerformerSearchRequest, PerformerUpsertRequest, PerformerUpsertRequest>
    {
        public PerformerController(ICRUDService<PerformerResponse, PerformerSearchRequest, PerformerUpsertRequest, PerformerUpsertRequest> service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
