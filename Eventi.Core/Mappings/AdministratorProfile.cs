using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Domain;

namespace Eventi.Core.Mappings
{
    public class AdministratorProfile : Profile
    {
        public AdministratorProfile()
        {
            CreateMap<AdministratorRegistrationRequest, Administrator>();
        }
    }
}
