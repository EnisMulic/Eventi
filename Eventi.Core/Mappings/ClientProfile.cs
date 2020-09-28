using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Domain;

namespace Eventi.Core.Mappings
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<ClientRegistrationRequest, Client>();
        }
    }
}
