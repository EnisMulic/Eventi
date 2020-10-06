using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Eventi.WebAPI.Controllers.V1
{
    [ApiController]
    public class AdministratorController : CRUDController<AdministratorResponse, AdministratorSearchRequest, object, AdministratorUpdateRequest>
    {
        public AdministratorController(IAdministratorService service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
