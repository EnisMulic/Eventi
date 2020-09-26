using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eventi.WebAPI.Controllers.V1
{
    [ApiController]
    public class EventController : CRUDController<EventResponse, EventSearchRequest, EventInsertRequest, EventUpdateRequest>
    {
        public EventController(ICRUDService<EventResponse, EventSearchRequest, EventInsertRequest, EventUpdateRequest> service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
