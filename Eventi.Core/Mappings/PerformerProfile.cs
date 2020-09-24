using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Domain;

namespace Eventi.Core.Mappings
{
    public class PerformerProfile : Profile
    {
        public PerformerProfile()
        {
            CreateMap<Performer, PerformerUpsertRequest>().ReverseMap();
            CreateMap<Performer, PerformerResponse>();
        }
    }
}
