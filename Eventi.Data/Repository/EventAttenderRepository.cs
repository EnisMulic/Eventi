using Eventi.Data.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Eventi.Data.Repository
{
    public class EventAttenderRepository<TEntity> : IEventAttenderRepository<TEntity> where TEntity : class
    {
        private readonly MojContext ctx;
        private readonly DbSet<TEntity> table;
        public EventAttenderRepository(MojContext context)
        {
            ctx = context;
            table = ctx.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            ctx.Add(entity);
            ctx.SaveChanges();
        }

        public TEntity Get(int id)
        {
            return table.Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return table;
        }


        public void Remove(int id)
        {
            TEntity existing = table.Find(id);
            table.Remove(existing);
            ctx.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            table.Attach(entity);
            ctx.SaveChanges();
        }

    }
}
