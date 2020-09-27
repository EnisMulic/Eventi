using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventi.Core.Interfaces
{
    public interface IEventService : ICRUDService<EventResponse, EventSearchRequest, EventInsertRequest, EventUpdateRequest>
    {
        public Task<List<PerformerResponse>> GetPerformers(int id);
        public Task<bool> AddPerformer(int eventId, int performerId);
        public Task<bool> DeletePerformer(int eventId, int performerId);
        public Task<List<SponsorResponse>> GetSponsors(int id);
        public Task<bool> AddSponsor(int eventId, EventSponsorInsertRequest request);
        public Task<bool> UpdateSponsor(int eventId, int sponsorId, EventSponsorUpdateRequest request);
        public Task<bool> DeleteSponsor(int eventId, int sponsorId);
    }
}
