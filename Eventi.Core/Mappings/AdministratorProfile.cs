using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Domain;

namespace Eventi.Core.Mappings
{
    public class AdministratorProfile : Profile
    {
        public AdministratorProfile()
        {
            CreateMap<AdministratorRegistrationRequest, Administrator>();
            CreateMap<AdministratorUpdateRequest, Administrator>();
            CreateMap<Administrator, AdministratorResponse>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Person.Account.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Person.Account.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Person.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Person.LastName));
        }
    }
}
