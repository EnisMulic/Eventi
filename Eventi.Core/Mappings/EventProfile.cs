using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Domain;

namespace Eventi.Core.Mappings
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventInsertRequest>().ReverseMap();
            CreateMap<Event, EventUpdateRequest>().ReverseMap();
            CreateMap<Event, EventResponse>();
        }
    }
}
