using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Domain;

namespace Eventi.Core.Mappings
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<AdministratorRegistrationRequest, Person>();
            CreateMap<ClientRegistrationRequest, Person>();
            CreateMap<ClientUpdateRequest, Person>();
        }
    }
}
