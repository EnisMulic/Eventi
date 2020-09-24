using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Domain;

namespace Eventi.Core.Mappings
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketInsertRequest>().ReverseMap();
            CreateMap<Ticket, TicketUpdateRequest>().ReverseMap();
            CreateMap<Ticket, TicketResponse>();
        }
    }
}
