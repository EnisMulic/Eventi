using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Eventi.Database;
using Eventi.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventi.Services
{
    public class PerformerService : CRUDService<PerformerResponse, PerformerSearchRequest, Performer, PerformerUpsertRequest, PerformerUpsertRequest>
    {
        private readonly EventiContext _context;
        private readonly IMapper _mapper;
        public PerformerService(EventiContext context, IMapper mapper, IUriService uriService) : base(context, mapper, uriService)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override IQueryable<Performer> ApplyFilter(IQueryable<Performer> query, PerformerSearchRequest search)
        {
            if (search != null)
            {
                if (!string.IsNullOrEmpty(search.Name))
                {
                    query = query.Where(i => i.Name.StartsWith(search.Name));
                }


                if (search.PerformerCategory != null)
                {
                    query = query.Where(i => i.PerformerCategory == search.PerformerCategory);
                }
            }

            return query;
        }
    }
}
