﻿
namespace TrashMob.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Identity.Web.Resource;
    using TrashMob.Common;
    using TrashMob.Models;
    using TrashMob.Persistence;

    [ApiController]
    [Route("api/events")]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository eventRepository;
        private readonly IEventAttendeeRepository eventAttendeeRepository;

        public EventsController(IEventRepository eventRepository, IEventAttendeeRepository eventAttendeeRepository)
        {
            this.eventRepository = eventRepository;
            this.eventAttendeeRepository = eventAttendeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var result = await eventRepository.GetAllEvents().ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet]
        [Route("active")]
        public async Task<IActionResult> GetActiveEvents()
        {
            var result = await eventRepository.GetActiveEvents().ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [RequiredScope(Constants.TrashMobReadScope)]
        [Route("eventsowned/{userId}")]
        public async Task<IActionResult> GetEventsUserOwns(Guid userId)
        {
            var result = await eventRepository.GetUserEvents(userId).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet]
        [Route("eventsuserisattending/{userId}")]
        [Authorize]
        [RequiredScope(Constants.TrashMobReadScope)]
        public async Task<IActionResult> GetEventsUserIsAttending(Guid userId)
        {
            var result = await eventAttendeeRepository.GetEventsUserIsAttending(userId).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(Guid id)
        {
            var mobEvent = await eventRepository.GetEvent(id).ConfigureAwait(false);

            if (mobEvent == null)
            {
                return NotFound();
            }

            return Ok(mobEvent);
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize]
        [RequiredScope(Constants.TrashMobWriteScope)]
        public async Task<IActionResult> PutEvent(Guid id, Event mobEvent)
        {
            try
            {
                var updatedEvent = await eventRepository.UpdateEvent(mobEvent).ConfigureAwait(false);
                return Ok(updatedEvent);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EventExists(id).ConfigureAwait(false))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Events
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize]
        [RequiredScope(Constants.TrashMobWriteScope)]
        public async Task<IActionResult> PostEvent(Event mobEvent)
        {
            var newEventId = await eventRepository.AddEvent(mobEvent).ConfigureAwait(false);

            return CreatedAtAction("GetEvent", new { id = newEventId }, mobEvent);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        [Authorize]
        [RequiredScope(Constants.TrashMobWriteScope)]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            await eventRepository.DeleteEvent(id).ConfigureAwait(false);
            return NoContent();
        }

        private async Task<bool> EventExists(Guid id)
        {
            return (await eventRepository.GetAllEvents().ConfigureAwait(false)).Any(e => e.Id == id);
        }
    }
}
