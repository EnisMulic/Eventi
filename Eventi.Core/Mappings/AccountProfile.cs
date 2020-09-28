using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventi.Core.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<RegistrationRequest, Account>();
            CreateMap<ClientRegistrationRequest, Account>();
            CreateMap<AdministratorRegistrationRequest, Account>();
            CreateMap<OrganizerRegistrationRequest, Account>();
        }
    }
}
