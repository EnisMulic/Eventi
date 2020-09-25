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
        public CountryService(EventiContext context, IMapper mapper, IUriService uriService) : base(context, mapper, uriService)
        {
           _context = context;
           _mapper = mapper;
        }

        public async override Task<PagedResponse<CountryResponse>> Get(CountrySearchRequest search, PaginationQuery pagination)
        {
            var query = _context.Countries
                .AsNoTracking()
                .AsQueryable();

            query = ApplyFilter(query, search);

            var skip = (pagination.PageNumber - 1) * pagination.PageSize;
            query = query.Skip(skip).Take(pagination.PageSize);

            var list = await query.ToListAsync();
            List<CountryResponse> dtoList = _mapper.Map<List<CountryResponse>>(list);

            var pagedResponse = await base.GetPagedResponse(dtoList, pagination);
            return pagedResponse;
        }

        private IQueryable<Country> ApplyFilter(IQueryable<Country> query, CountrySearchRequest search)
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
