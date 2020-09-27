using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Eventi.Database;
using Eventi.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventi.Services
{
    public class CountryService : CRUDService<CountryResponse, CountrySearchRequest, Country, CountryUpsertRequest, CountryUpsertRequest>, ICountryService
    {
        private readonly EventiContext _context;
        private readonly IMapper _mapper;
        private readonly ICityService _cityService;
        public CountryService(EventiContext context, IMapper mapper, IUriService uriService, ICityService cityService) : base(context, mapper, uriService)
        {
            _context = context;
            _mapper = mapper;
            _cityService = cityService;
        }

        public async Task<List<CityResponse>> GetCityAsync(int id)
        {
            var query = _context.Cities
                .AsNoTracking()
                .Where(i => i.CountryID == id)
                .AsQueryable();

            var list = await query.ToListAsync();

            return _mapper.Map<List<CityResponse>>(list);
        }

        protected override IQueryable<Country> ApplyFilter(IQueryable<Country> query, CountrySearchRequest search)
        {
            if (search != null)
            {
                if (!string.IsNullOrEmpty(search.Name))
                {
                    query = query.Where(i => i.Name.StartsWith(search.Name));
                }
            }

            return query;
        }
    }
}
