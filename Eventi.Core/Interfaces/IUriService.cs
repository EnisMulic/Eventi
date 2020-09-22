using System;
using Eventi.Contracts.V1.Requests;

namespace Eventi.Core.Interfaces
{
    public interface IUriService
    {
        public Uri GetUri(PaginationQuery pagination = null);
    }
}
