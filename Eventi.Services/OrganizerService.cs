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
    public class OrganizerService : 
        CRUDService<OrganizerResponse, OrganizerSearchRequest, Organizer, OrganizerInsertRequest, OrganizerUpdateRequest>, 
        IOrganizerService
    {
        private readonly EventiContext _context;
        private readonly IMapper _mapper;
        private readonly IEventService _eventService;

        public OrganizerService(EventiContext context, IMapper mapper, IUriService uriService, IEventService eventService) : base(context, mapper, uriService)
        {
            _context = context;
            _mapper = mapper;
            _eventService = eventService;
        }

        public async override Task<PagedResponse<OrganizerResponse>> Get(OrganizerSearchRequest search, PaginationQuery pagination)
        {
            var query = _context.Organizers
                .Include(i => i.Account)
                .AsNoTracking()
                .AsQueryable();

            query = ApplyFilter(query, search);
            query = ApplyPagination(query, pagination);

            var list = await query.ToListAsync();
            var listDto = _mapper.Map<List<OrganizerResponse>>(list);
            var pagedResponse = await GetPagedResponse<OrganizerResponse, Organizer>(listDto, pagination);
            return pagedResponse;
        }

        public async Task<PagedResponse<EventResponse>> GetEvents(int ID, EventSearchRequest search, PaginationQuery pagination)
        {
            var query = _context.Events
                .Where(i => i.OrganizerID == ID)
                .AsNoTracking()
                .AsQueryable();

            
            query = ApplyPagination(query, pagination);

            var list = await query.ToListAsync();
            var listDto = _mapper.Map<List<EventResponse>>(list);
            var pagedResponse = await GetPagedResponse<EventResponse, Event>(listDto, pagination);
            return pagedResponse;
        }

        public override IQueryable<Organizer> ApplyFilter(IQueryable<Organizer> query, OrganizerSearchRequest search)
        {
            if(search != null)
            {
                if (search.AccountID != null)
                {
                    return query.Where(i => i.AccountID == search.AccountID);
                }

                if (!string.IsNullOrEmpty(search.Name))
                {
                    query = query.Where(i => i.Name.StartsWith(search.Name));
                }

                if(!string.IsNullOrEmpty(search.PhoneNumber))
                {
                    query = query.Where(i => i.PhoneNumber.StartsWith(search.PhoneNumber));
                }

                if(!string.IsNullOrEmpty(search.Email))
                {
                    query = query.Where(i => i.Account.Email.StartsWith(search.Email));
                }
            }

            return query;
        }
    }
}
