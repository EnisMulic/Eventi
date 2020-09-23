using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Domain;

namespace Eventi.Core.Mappings
{
    public class SponsorProfile : Profile
    {
        public SponsorProfile()
        {
            CreateMap<Sponsor, SponsorUpsertRequest>().ReverseMap();
            CreateMap<Sponsor, SponsorResponse>();
        }
    }
}
