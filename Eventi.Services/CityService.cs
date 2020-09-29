using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Eventi.Database;
using Eventi.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventi.Services
{
    public class CityService : CRUDService<CityResponse, CitySearchRequest, City, CityUpsertRequest, CityUpsertRequest>, ICityService
    {
        private readonly EventiContext _context;
        private readonly IMapper _mapper;
        public CityService(EventiContext context, IMapper mapper, IUriService uriService) : base(context, mapper, uriService)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResponse<EventResponse>> GetEventAsync(int id, PaginationQuery pagination)
        {
            var query = _context.Events
                .AsNoTracking()
                .Where(i => i.Venue.CityID == id)
                .AsQueryable();

            query = ApplyPagination(query, pagination);

            var list = await query.ToListAsync();
            var listDto = _mapper.Map<List<EventResponse>>(list);
            var pagedResponse = await GetPagedResponse<EventResponse, Event>(listDto, pagination);
            return pagedResponse;
        }

        public async Task<PagedResponse<VenueResponse>> GetVenueAsync(int id, PaginationQuery pagination)
        {
            var query = _context.Venues
                .AsNoTracking()
                .Where(i => i.CityID == id)
                .AsQueryable();

            query = ApplyPagination(query, pagination);

            var list = await query.ToListAsync();
            var listDto = _mapper.Map<List<VenueResponse>>(list);
            var pagedResponse = await GetPagedResponse<VenueResponse, Venue>(listDto, pagination);
            return pagedResponse;
        }

        public override IQueryable<City> ApplyFilter(IQueryable<City> query, CitySearchRequest search)
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
