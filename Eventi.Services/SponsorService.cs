using AutoMapper;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Eventi.Database;
using Eventi.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Eventi.Services
{
    public class SponsorService : CRUDService<SponsorResponse, SponsorSearchRequest, Sponsor, SponsorUpsertRequest, SponsorUpsertRequest>
    {
        public SponsorService(EventiContext context, IMapper mapper, IUriService uriService) : base(context, mapper, uriService)
        {
        }

        protected override IQueryable<Sponsor> ApplyFilter(IQueryable<Sponsor> query, SponsorSearchRequest search)
        {
            if(search != null)
            {
                if(!string.IsNullOrEmpty(search.Name))
                {
                    query = query.Where(i => i.Name.StartsWith(search.Name));
                }

                if (!string.IsNullOrEmpty(search.Email))
                {
                    query = query.Where(i => i.Email.StartsWith(search.Email));
                }

                if (!string.IsNullOrEmpty(search.PhoneNumber))
                {
                    query = query.Where(i => i.PhoneNumber.StartsWith(search.PhoneNumber));
                }
            }


            return query;
        }
    }
}
