using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Domain;

namespace Eventi.Core.Mappings
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityUpsertRequest>().ReverseMap();
            CreateMap<City, CityResponse>();
        }
    }
}
