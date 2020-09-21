﻿using System.Threading.Tasks;

namespace Template.Core.Interfaces
{
    public interface ICRUDService<T, TSearch, TInsert, TUpdate> : IBaseService<T, TSearch>
    {
        Task<T> Insert(TInsert request);
        Task<T> Update(string id, TUpdate request);
        Task<bool> Delete(string id);
    }
}
