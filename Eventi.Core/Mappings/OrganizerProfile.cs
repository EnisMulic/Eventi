using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Domain;

namespace Eventi.Core.Mappings
{
    public class OrganizerProfile : Profile
    {
        public OrganizerProfile()
        {
            CreateMap<Organizer, OrganizerInsertRequest>().ReverseMap();
            CreateMap<Organizer, OrganizerUpdateRequest>().ReverseMap();
            CreateMap<Organizer, OrganizerResponse>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Account.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Account.Email));
            CreateMap<OrganizerRegistrationRequest, Organizer>();
        }
    }
}
