using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Eventi.Database;
using Eventi.Domain;

namespace Eventi.Services
{
    public class CountryService : CRUDService<CountryResponse, CountrySearchRequest, Country, CountryUpsertRequest, CountryUpsertRequest>, ICountryService
    {
        public CountryService(EventiContext context, IMapper mapper, IUriService uriService) : base(context, mapper, uriService)
        {
        }
    }
}
