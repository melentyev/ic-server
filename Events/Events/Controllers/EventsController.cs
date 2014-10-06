using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;

using Events.Models;
using Events.Abstract;
using Events.Filters;

namespace Events.Controllers
{
    [Authorize]
    public class EventsController : ApiController
    {
        private IEventsRepository eventsRepository;
        //private ICommentsRepository commentsRepository;
        public EventsController(IEventsRepository evRepo) {
            eventsRepository = evRepo;
        }

        // GET api/Events
        public IQueryable<Event> GetEvents()
        {
            return eventsRepository.Objects;
        }

        // GET api/Events/5
        [ResponseType(typeof(Event))]
        public async Task<IHttpActionResult> GetEvent(int id)
        {

            Event @event = await eventsRepository.Objects.Where(e => e.EventId == id).FirstOrDefaultAsync();
            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }

        /*// PUT api/Events/5
        public async Task<IHttpActionResult> PutEvent(int id, Event ev)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != ev.EventId)
            {
                return BadRequest();
            }
            await repository.SaveEvent(ev);
            return StatusCode(HttpStatusCode.NoContent);
        }*/

        // POST api/Events
        [ResponseType(typeof(Event))]
        [CheckModelForNull]
        public async Task<IHttpActionResult> PostEvent(AddEventBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ev = new Event
            {
                UserId = User.Identity.GetUserId(),
                Latitude = model.Latitude,
                Longitude =  model.Longitude,
                Description = model.Description,
                EventDate = model.EventDate,
                DateCreate = DateTime.Now
            };
            await eventsRepository.SaveInstance(ev);

            return CreatedAtRoute("DefaultApi", new { id = ev.EventId }, ev);
        }

        // DELETE api/Events/5
        [ResponseType(typeof(Event))]
        public async Task<IHttpActionResult> DeleteEvent(int id)
        {
            Event @event = await eventsRepository.FindAsync(id);

            /*if (@event == null)
            {
                return NotFound();
            }

            db.Events.Remove(@event);
            await db.SaveChangesAsync();
            */
            return Ok(@event);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                eventsRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        private Task<bool> EventExists(int id)
        {
            return eventsRepository.Objects.Select(e => e.EventId).ContainsAsync(id);
        }
    }
}