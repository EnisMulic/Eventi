using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Eventi.Contracts.V1;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Eventi.WebAPI.Controllers.V1
{
    public class EventController : CRUDController<EventResponse, EventSearchRequest, EventInsertRequest, EventUpdateRequest>
    {
        private readonly IEventService _service;
        public EventController(IEventService service, IMapper mapper) : base(service, mapper)
        {
            _service = service;
        }

        [HttpGet("{id}/Performer")]
        public async Task<ActionResult<List<PerformerResponse>>> GetPerformersAsync(int id)
        {
            var response = await _service.GetPerformers(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("{eventId}/Performer/{performerId}")]
        public async Task<ActionResult> AddPerformerAsync(int eventId, int performerId)
        {
            var response = await _service.AddPerformer(eventId, performerId);

            if(!response)
            {
                var errorModel = new ErrorModel()
                {
                    Message = "Performer is already added to the Event"
                };
                var error = new ErrorResponse(errorModel);

                return Conflict(error);
            }
            
            return Ok();
        }

        [HttpDelete("{eventId}/Performer/{performerId}")]
        public async Task<ActionResult> DeletePerformerAsync(int eventId, int performerId)
        {
            var response = await _service.DeletePerformer(eventId, performerId);

            if(!response)
            {
                return NotFound();
            }

            return Ok();
        }


        [HttpGet("{id}/Sponsor")]
        public async Task<ActionResult<List<SponsorResponse>>> GetSponsorsAsync(int id)
        {
            var response = await _service.GetSponsors(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("{id}/Sponsor")]
        public async Task<ActionResult> AddSponsorAsync(int id, EventSponsorInsertRequest request)
        {
            var response = await _service.AddSponsor(id, request);

            if (!response)
            {
                var errorModel = new ErrorModel()
                {
                    Message = "Sponsor is already added to the Event"
                };
                var error = new ErrorResponse(errorModel);

                return Conflict(error);
            }

            return Ok();
        }

        [HttpPut("{eventId}/Sponsor/{sponsorId}")]
        public async Task<ActionResult> UpdateSponsorAsync(int eventId, int sponsorId, EventSponsorUpdateRequest request)
        {
            var response = await _service.UpdateSponsor(eventId, sponsorId, request);

            if (!response)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{eventId}/Sponsor/{sponsorId}")]
        public async Task<ActionResult> DeleteSponsorAsync(int eventId, int sponsorId)
        {
            var response = await _service.DeleteSponsor(eventId, sponsorId);

            if (!response)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
