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
    public class BaseService<TModel, TSearch, TDatabase> : IBaseService<TModel, TSearch>
        where TDatabase : class
        where TModel : class
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
            var pagedResponse = await GetPagedResponse<TModel, TDatabase>(_mapper.Map<List<TModel>>(list), pagination);
            return pagedResponse;
        }

        public virtual IQueryable<TDatabase> ApplyFilter(IQueryable<TDatabase> query, TSearch search)
        {
            return query;
        }

        protected IQueryable<T> ApplyPagination<T>(IQueryable<T> query, PaginationQuery pagination) where T : class
        {
            var skip = (pagination.PageNumber - 1) * pagination.PageSize;
            query = query.Skip(skip).Take(pagination.PageSize);

            return query;
        }

        protected async Task<PagedResponse<T>> GetPagedResponse<T, TDb>(List<T> list, PaginationQuery pagination) 
            where T : class 
            where TDb : class
        {
            int count = await _context.Set<TDb>()
                .AsNoTracking()
                .CountAsync();

            return PaginationHelper.CreatePaginatedResponse(_uriService, pagination, list, count);
        }

        public virtual async Task<TModel> GetById(int id)
        {
            var entity = await _context.Set<TDatabase>().FindAsync(id);
            return _mapper.Map<TModel>(entity);
        }
    }
}
