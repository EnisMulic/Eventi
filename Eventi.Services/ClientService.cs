using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Eventi.Database;
using Eventi.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventi.Services
{
    public class ClientService : CRUDService<ClientResponse, ClientSearchRequest, Client, object, ClientUpdateRequest>, IClientService
    {
        private readonly EventiContext _context;
        private readonly IMapper _mapper;
        public ClientService(EventiContext context, IMapper mapper, IUriService uriService) : base(context, mapper, uriService)
        {
            _context = context;
            _mapper = mapper;
        }

        public override IQueryable<Client> ApplyFilter(IQueryable<Client> query, ClientSearchRequest search)
        {
            if(search != null)
            {
                if(!string.IsNullOrEmpty(search.FirstName))
                {
                    query = query.Where(i => i.Person.FirstName == search.FirstName);
                }

                if (!string.IsNullOrEmpty(search.LastName))
                {
                    query = query.Where(i => i.Person.LastName == search.LastName);
                }

                if (!string.IsNullOrEmpty(search.Email))
                {
                    query = query.Where(i => i.Person.Account.Email == search.Email);
                }

                if (!string.IsNullOrEmpty(search.Username))
                {
                    query = query.Where(i => i.Person.Account.Username == search.Username);
                }

                if (!string.IsNullOrEmpty(search.Address))
                {
                    query = query.Where(i => i.Address == search.Address);
                }

            }

            return query;
        }

        public async override Task<PagedResponse<ClientResponse>> Get(ClientSearchRequest search, PaginationQuery pagination)
        {
            var query = _context.Clients
                .AsNoTracking()
                .Include(i => i.Person)
                .ThenInclude(i => i.Account)
                .AsQueryable();

            query = ApplyFilter(query, search);
            query = ApplyPagination(query, pagination);

            var list = await query.ToListAsync();
            var listDto = _mapper.Map<List<ClientResponse>>(list);
            var pagedResponse = await GetPagedResponse<ClientResponse, Client>(listDto, pagination);

            return pagedResponse;
        }

        public async override Task<ClientResponse> GetById(int id)
        {
            var entity = await _context.Clients
                .AsNoTracking()
                .Include(i => i.Person)
                .ThenInclude(i => i.Account)
                .Where(i => i.ID == id)
                .SingleOrDefaultAsync();

            return _mapper.Map<ClientResponse>(entity);
        }

        public async Task<List<EventResponse>> GetEvents(int id)
        {
            var query = _context.Purchases
                .AsNoTracking()
                .Include(i => i.Ticket)
                .ThenInclude(i => i.Event)
                .Where(i => i.ClientID == id)
                .Select(i => i.Ticket.Event)
                .AsQueryable();

            var list = await query.ToListAsync();
            return _mapper.Map<List<EventResponse>>(list);
        }

        public override Task<ClientResponse> Insert(object request)
        {
            throw new NotImplementedException();
        }

        public async override Task<ClientResponse> Update(int id, ClientUpdateRequest request)
        {
            var entity = await _context.Clients
                .Include(i => i.Person)
                .ThenInclude(i => i.Account)
                .Where(i => i.ID == id)
                .SingleOrDefaultAsync();

            if(entity != null)
            {
                _context.Attach(entity);
                _context.Update(entity);

                _mapper.Map(request, entity);
                _mapper.Map(request, entity.Person);
                _mapper.Map(request, entity.Person.Account);

                await _context.SaveChangesAsync();
            }

            return _mapper.Map<ClientResponse>(entity);
        }
    }
}
