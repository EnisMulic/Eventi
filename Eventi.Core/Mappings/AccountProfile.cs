using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Domain;

namespace Eventi.Core.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountResponse>();
            CreateMap<RegistrationRequest, Account>();
            CreateMap<ClientRegistrationRequest, Account>();
            CreateMap<AdministratorRegistrationRequest, Account>();
            CreateMap<OrganizerRegistrationRequest, Account>();
            CreateMap<ClientUpdateRequest, Account>();
        }
    }
}
