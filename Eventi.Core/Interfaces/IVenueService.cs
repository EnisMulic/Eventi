using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventi.Core.Interfaces
{
    public interface IVenueService : ICRUDService<VenueResponse, VenueSearchRequest, VenueUpsertRequest, VenueUpsertRequest>
    {
    }
}
