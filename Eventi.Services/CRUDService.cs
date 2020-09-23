﻿using AutoMapper;
using System;
using System.Threading.Tasks;
using Eventi.Core.Interfaces;
using Eventi.Database;
using Eventi.Services;

namespace Eventi.Services
{
    public class CRUDService<TModel, TSearch, TDatabase, TInsert, TUpdate> :
        BaseService<TModel, TSearch, TDatabase>, ICRUDService<TModel, TSearch, TInsert, TUpdate>
        where TDatabase : class
    {
        private readonly EventiContext _context;
        private readonly IMapper _mapper;
        public CRUDService(EventiContext context, IMapper mapper, IUriService uriService) : base(context, mapper, uriService)
        {
            _context = context;
            _mapper = mapper;
        }
        public virtual async Task<TModel> Insert(TInsert request)
        {
            var entity = _mapper.Map<TDatabase>(request);

            _context.Set<TDatabase>().Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task<TModel> Update(string id, TUpdate request)
        {
            var entity = _context.Set<TDatabase>().Find(id);
            _context.Set<TDatabase>().Attach(entity);
            _context.Set<TDatabase>().Update(entity);

            _mapper.Map(request, entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task<bool> Delete(string id)
        {
            var entity = await _context.Set<TDatabase>().FindAsync(id);

            try
            {
                _context.Set<TDatabase>().Remove(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}