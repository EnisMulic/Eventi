using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Eventi.Database;
using Eventi.Domain;

namespace Eventi.Services
{
    public class PerformerService : CRUDService<PerformerResponse, PerformerSearchRequest, Performer, PerformerUpsertRequest, PerformerUpsertRequest>
    {
        public PerformerService(EventiContext context, IMapper mapper, IUriService uriService) : base(context, mapper, uriService)
        {
        }
    }
}
