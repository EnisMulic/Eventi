using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventi.Core.Interfaces
{
    public interface ICountryService : ICRUDService<CountryResponse, CountrySearchRequest, CountryUpsertRequest, CountryUpsertRequest>
    {
        public Task<List<CityResponse>> GetCityAsync(int id);
    }
}
