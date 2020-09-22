using System.Threading.Tasks;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;

namespace Eventi.Core.Interfaces
{
    public interface IBaseService<T, TSearch>
    {
        Task<PagedResponse<T>> Get(TSearch search, PaginationQuery pagination);
        Task<T> GetById(string id);
    }
}
