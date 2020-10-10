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
            CreateMap<ClientResponse, Account>();
            CreateMap<AdministratorResponse, Account>();
            CreateMap<OrganizerResponse, Account>();
        }
    }
}
