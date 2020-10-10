using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Domain;

namespace Eventi.Core.Mappings
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<ClientRegistrationRequest, Client>();
            CreateMap<ClientUpdateRequest, Client>();
            CreateMap<Client, ClientResponse>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Person.Account.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Person.Account.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Person.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Person.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Person.PhoneNumber))
                .ForMember(dest => dest.AccountID, opt => opt.MapFrom(src => src.Person.AccountID));
        }
    }
}
