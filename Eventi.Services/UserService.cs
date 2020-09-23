using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Eventi.Database;
using Eventi.Domain;

namespace Eventi.Services
{
    public class UserService : CRUDService<UserResponse, UserSearchRequest, Person, UserInsertRequest, UserUpdateRequest>
    {
        private readonly EventiContext _context;
        private readonly UserManager<Person> _userManager;
        private readonly IMapper _mapper;

        public UserService(EventiContext context, UserManager<Person> userManager, IMapper mapper, IUriService uriService)
            : base(context, mapper, uriService)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        //public async override Task<PagedResponse<UserResponse>> Get(UserSearchRequest search, PaginationQuery pagination)
        //{
        //    var query = _context.Set<Person>().AsQueryable();

        //    query = ApplyFilterToQuery(query, search);


        //    var skip = (pagination.PageNumber - 1) * pagination.PageSize;
        //    query = query.Skip(skip).Take(pagination.PageSize);


        //    var list = await query.ToListAsync();
        //    var pagedList = await base.GetPagedResponse(_mapper.Map<List<UserResponse>>(list), pagination);
        //    return pagedList;
        //}

        //public async override Task<UserResponse> Insert(UserInsertRequest request)
        //{
        //    var user = _mapper.Map<Person>(request);
        //    await _userManager.CreateAsync(user, request.Password);

        //    //await _context.AddAsync(user);
        //    await _context.SaveChangesAsync();

        //    return _mapper.Map<UserResponse>(user);
        //}

        //private IQueryable<Person> ApplyFilterToQuery(IQueryable<Person> query, UserSearchRequest filter)
        //{
        //    //if(!string.IsNullOrEmpty(filter?.Id))
        //    //{
        //    //    query = query.Where(i => i.Id == filter.Id);
        //    //}

        //    //if(!string.IsNullOrEmpty(filter?.UserName))
        //    //{
        //    //    query = query.Where(i => i.UserName == filter.UserName);
        //    //}

        //    //if (!string.IsNullOrEmpty(filter?.Email))
        //    //{
        //    //    query = query.Where(i => i.Email == filter.Email);
        //    //}

        //    //if (!string.IsNullOrEmpty(filter?.PhoneNumber))
        //    //{
        //    //    query = query.Where(i => i.PhoneNumber == filter.PhoneNumber);
        //    //}

        //    return query;
        //}
    }
}
