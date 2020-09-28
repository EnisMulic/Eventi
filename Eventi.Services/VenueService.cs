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
    public class VenueService : CRUDService<VenueResponse, VenueSearchRequest, Venue, VenueUpsertRequest, VenueUpsertRequest>, IVenueService
    {
        private readonly EventiContext _context;
        private readonly IMapper _mapper;
        public VenueService(EventiContext context, IMapper mapper, IUriService uriService) : base(context, mapper, uriService)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResponse<EventResponse>> GetEventAsync(int id, PaginationQuery pagination)
        {
            var query = _context.Events
                .AsNoTracking()
                .Where(i => i.VenueID == id)
                .AsQueryable();

            query = ApplyPagination(query, pagination);

            var list = await query.ToListAsync();
            var listDto = _mapper.Map<List<EventResponse>>(list);
            var pagedResponse = await GetPagedResponse<EventResponse, Event>(listDto, pagination);
            return pagedResponse;
        }

        protected override IQueryable<Venue> ApplyFilter(IQueryable<Venue> query, VenueSearchRequest search)
        {
            if(search != null)
            {
                if(!string.IsNullOrEmpty(search.Name))
                {
                    query = query.Where(i => i.Name.StartsWith(search.Name));
                }
                
                if(!string.IsNullOrEmpty(search.Address))
                {
                    query = query.Where(i => i.Address.StartsWith(search.Address));
                }

                if(search.VenueCategory != null)
                {
                    query = query.Where(i => i.VenueCategory == search.VenueCategory);
                }
            }

            return query;
        }
    }
}
