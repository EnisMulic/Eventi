using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Domain;

namespace Eventi.Core.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Person, UserInsertRequest>().ReverseMap();
            CreateMap<Person, UserUpdateRequest>().ReverseMap();
            CreateMap<Person, UserResponse>();
        }
    }
}
