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
using System.Text;
using System.Threading.Tasks;

namespace Eventi.Services
{
    public class AdministratorService :
        CRUDService<AdministratorResponse, AdministratorSearchRequest, Administrator, object, AdministratorUpdateRequest>,
        IAdministratorService
    {
        private readonly EventiContext _context;
        private readonly IMapper _mapper;
        public AdministratorService(EventiContext context, IMapper mapper, IUriService uriService) : base(context, mapper, uriService)
        {
            _context = context;
            _mapper = mapper;
        }

        public override IQueryable<Administrator> ApplyFilter(IQueryable<Administrator> query, AdministratorSearchRequest search)
        {
            if (search != null)
            {
                if (search.AccountID != null)
                {
                    return query.Where(i => i.Person.AccountID == search.AccountID);
                }

                if (!string.IsNullOrEmpty(search.FirstName))
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
            }

            return query;
        }

        public async override Task<PagedResponse<AdministratorResponse>> Get(AdministratorSearchRequest search, PaginationQuery pagination)
        {
            var query = _context.Administrators
                .AsNoTracking()
                .Include(i => i.Person)
                .ThenInclude(i => i.Account)
                .AsQueryable();

            query = ApplyFilter(query, search);
            query = ApplyPagination(query, pagination);

            var list = await query.ToListAsync();
            var listDto = _mapper.Map<List<AdministratorResponse>>(list);
            var pagedResponse = await GetPagedResponse<AdministratorResponse, Administrator>(listDto, pagination);

            return pagedResponse;
        }

        public async override Task<AdministratorResponse> GetById(int id)
        {
            var entity = await _context.Administrators
                .AsNoTracking()
                .Include(i => i.Person)
                .ThenInclude(i => i.Account)
                .Where(i => i.ID == id)
                .SingleOrDefaultAsync();

            return _mapper.Map<AdministratorResponse>(entity);
        }

        public override Task<AdministratorResponse> Insert(object request)
        {
            throw new NotImplementedException();
        }

        public async override Task<AdministratorResponse> Update(int id, AdministratorUpdateRequest request)
        {
            var entity = await _context.Administrators
                .Include(i => i.Person)
                .ThenInclude(i => i.Account)
                .Where(i => i.ID == id)
                .SingleOrDefaultAsync();

            if (entity != null)
            {
                _context.Attach(entity);
                _context.Update(entity);

                _mapper.Map(request, entity);
                _mapper.Map(request, entity.Person);
                _mapper.Map(request, entity.Person.Account);

                await _context.SaveChangesAsync();
            }

            return _mapper.Map<AdministratorResponse>(entity);
        }
    }
}
