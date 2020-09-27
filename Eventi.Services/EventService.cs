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
    public class EventService : CRUDService<EventResponse, EventSearchRequest, Event, EventInsertRequest, EventUpdateRequest>, IEventService
    {
        private readonly EventiContext _context;
        private readonly IMapper _mapper;
        public EventService(EventiContext context, IMapper mapper, IUriService uriService) : base(context, mapper, uriService)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PerformerResponse>> GetPerformers(int id)
        {
            var query = _context.EventPerformers
                .Where(i => i.EventID == id)
                .Select(i => i.Performer)
                .AsNoTracking()
                .AsQueryable();


            var list = await query.ToListAsync();
            return _mapper.Map<List<PerformerResponse>>(list);
        }
        public async Task<bool> AddPerformer(int eventId, int performerId)
        {
            var entity = new EventPerformer
            {
                EventID = eventId,
                PerformerID = performerId
            };

            try
            {
                await _context.EventPerformers.AddAsync(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeletePerformer(int eventId, int performerId)
        {
            var entity = await _context.EventPerformers
                .Where(i => i.EventID == eventId && i.PerformerID == performerId)
                .SingleOrDefaultAsync();

            if(entity != null)
            {
                _context.EventPerformers.Remove(entity);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        
        public async Task<List<SponsorResponse>> GetSponsors(int id)
        {
            var query = _context.EventSponsors
                .Where(i => i.EventID == id)
                .Select(i => i.Sponsor)
                .AsNoTracking()
                .AsQueryable();


            var list = await query.ToListAsync();
            return _mapper.Map<List<SponsorResponse>>(list);
        }

        public async Task<bool> AddSponsor(int eventId, EventSponsorInsertRequest request)
        {
            var entity = new EventSponsor
            {
                EventID = eventId,
                SponsorID = request.SponsorID,
                SponsorCategory = request.SponsorCategory
            };

            try
            {
                await _context.EventSponsors.AddAsync(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateSponsor(int eventId, int sponsorId, EventSponsorUpdateRequest request)
        {
            var entity = await _context.EventSponsors
                .Where(i => i.EventID == eventId && i.SponsorID == sponsorId)
                .FirstOrDefaultAsync();

            if(entity != null)
            {
                _context.EventSponsors.Attach(entity);
                _context.EventSponsors.Update(entity);

                entity.SponsorCategory = request.SponsorCategory;

                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteSponsor(int eventId, int sponsorId)
        {
            var entity = await _context.EventSponsors
                .Where(i => i.EventID == eventId && i.SponsorID == sponsorId)
                .SingleOrDefaultAsync();

            if (entity != null)
            {
                _context.EventSponsors.Remove(entity);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        protected override IQueryable<Event> ApplyFilter(IQueryable<Event> query, EventSearchRequest search)
        {
            if(search != null)
            {
                if(!string.IsNullOrEmpty(search.Name))
                {
                    query = query.Where(i => i.Name.StartsWith(search.Name));
                }

                if(search.EventCategory != null)
                {
                    query = query.Where(i => i.EventCategory == search.EventCategory);
                }

                if(search.VenueID != null)
                {
                    query = query.Where(i => i.VenueID == search.VenueID);
                }

                if(search.IsApproved != null)
                {
                    query = query.Where(i => i.IsApproved == search.IsApproved);
                }

                if (search.IsCanceled != null)
                {
                    query = query.Where(i => i.IsCanceled == search.IsCanceled);
                }

                if(search.Start != null)
                {
                    query = query.Where(i => i.Start >= search.Start);
                }

                if (search.End != null)
                {
                    query = query.Where(i => i.End <= search.End);
                }
            }

            return query;
        }

        
    }
}
