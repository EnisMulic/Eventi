using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;

namespace Eventi.WebAPI.Controllers.V1
{
    public class CountryController : CRUDController<CountryResponse, CountrySearchRequest, CountryUpsertRequest, CountryUpsertRequest>
    {
        public CountryController(ICountryService service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
