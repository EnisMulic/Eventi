using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Event_Attender.Data.Repository
{
    interface IEventAttenderRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IQueryable<TEntity> GetAll();
        void Add(TEntity entity);
        void Remove(int id);
        void Update(TEntity entity);
    }
}
