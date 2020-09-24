using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Eventi.Database;
using Eventi.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventi.Services
{
    public class CityService : CRUDService<CityResponse, CitySearchRequest, City, CityUpsertRequest, CityUpsertRequest>, ICityService
    {
        public CityService(EventiContext context, IMapper mapper, IUriService uriService) : base(context, mapper, uriService)
        {
        }
    }
}
