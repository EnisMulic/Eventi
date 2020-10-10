using AutoMapper;
using Eventi.Contracts.V1.Responses;
using Eventi.Web.Models;

namespace Eventi.Web.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<AccountResponse, Account>();
            CreateMap<ClientResponse, Account>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.AccountID));
            CreateMap<AdministratorResponse, Account>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.AccountID));
            CreateMap<OrganizerResponse, Account>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.AccountID));
        }
    }
}
