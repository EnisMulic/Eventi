using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;

namespace Eventi.WebAPI.Controllers.V1
{

    [ApiController]
    public class UserController : CRUDController<UserResponse, UserSearchRequest, UserInsertRequest, UserInsertRequest>
    {
        public UserController(ICRUDService<UserResponse, UserSearchRequest, UserInsertRequest, UserInsertRequest> service, IMapper mapper)
            : base(service, mapper)
        {
        }
    }
}