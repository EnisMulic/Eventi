using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Helpers;
using Eventi.Core.Interfaces;
using Eventi.Database;

namespace Eventi.Services
{
    [Authorize]
    public class BaseService<TModel, TSearch, TDatabase> : IBaseService<TModel, TSearch> where TDatabase: class
    {
        private readonly EventiContext _context;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public BaseService(EventiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BaseService(EventiContext context, IMapper mapper, IUriService uriService)
        {
            _context = context;
            _mapper = mapper;
            _uriService = uriService;
        }

        public virtual async Task<PagedResponse<TModel>> Get(TSearch search, PaginationQuery pagination)
        {
            var query = _context.Set<TDatabase>()
                .AsNoTracking()
                .AsQueryable();


            query = ApplyFilter(query, search);
            query = ApplyPagination(query, pagination);
            

            var list = await query.ToListAsync();
            var pagedResponse = await GetPagedResponse(_mapper.Map<List<TModel>>(list), pagination);
            return pagedResponse;
        }

        protected virtual IQueryable<TDatabase> ApplyFilter(IQueryable<TDatabase> query, TSearch search)
        {
            return query;
        }

        protected IQueryable<T> ApplyPagination<T>(IQueryable<T> query, PaginationQuery pagination)
        {
            var skip = (pagination.PageNumber - 1) * pagination.PageSize;
            query = query.Skip(skip).Take(pagination.PageSize);

            return query;
        }

        protected async Task<PagedResponse<TModel>> GetPagedResponse(List<TModel> list, PaginationQuery pagination)
        {
            int count = await _context.Set<TDatabase>()
                .AsNoTracking()
                .CountAsync();

            return PaginationHelper.CreatePaginatedResponse(_uriService, pagination, list, count);
        }

        public virtual async Task<TModel> GetById(string id)
        {
            var entity = await _context.Set<TDatabase>().FindAsync(id);
            return _mapper.Map<TModel>(entity);
        }
    }
}
