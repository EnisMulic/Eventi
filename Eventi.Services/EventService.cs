using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Eventi.Database;
using Eventi.Domain;
using System.Linq;

namespace Eventi.Services
{
    public class EventService : CRUDService<EventResponse, EventSearchRequest, Event, EventInsertRequest, EventUpdateRequest>
    {
        public EventService(EventiContext context, IMapper mapper, IUriService uriService) : base(context, mapper, uriService)
        {
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
