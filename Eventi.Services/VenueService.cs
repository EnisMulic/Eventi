using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Eventi.Database;
using Eventi.Domain;

namespace Eventi.Services
{
    public class VenueService : CRUDService<VenueResponse, VenueSearchRequest, Venue, VenueUpsertRequest, VenueUpsertRequest>, IVenueService
    {
        public VenueService(EventiContext context, IMapper mapper, IUriService uriService) : base(context, mapper, uriService)
        {
        }
    }
}
