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

using Events.Infrastructure;
using Events.Models;
using Events.Abstract;
using Events.Filters;
using GlobalHost = Microsoft.AspNet.SignalR.GlobalHost;

namespace Events.Controllers
{
    //[Authorize]
    public class EventsController : ApplicationApiController
    {
        private IEventsRepository eventsRepository;
        private IGcmRegIdsRepository gcmRepo;
        private const int getEventsMaxCount = 200;
        //private ICommentsRepository commentsRepository;

        public EventsController(IEventsRepository evRepo, IGcmRegIdsRepository paramGcmRepo)
        {
            eventsRepository = evRepo;
            gcmRepo = paramGcmRepo;
        }
        // GET api/Events
        public IQueryable<EventViewModel> GetEvents(int? offset = null, int count = 20)
        {
            var query = eventsRepository.Objects;
            query = offset == null ? query : query.Skip(offset.Value);
            query = query.Take(Math.Min(count, getEventsMaxCount));
            return query.Select(e => new EventViewModel 
            {   
                EventId = e.EventId, 
                UserId = e.UserId,
                Latitude = e.Latitude,
                Longitude = e.Longitude,
                Description = e.Description,
                EventDate = e.EventDate,
            });;
        }

        // GET api/Events/5
        [ResponseType(typeof(EventViewModel))]
        public async Task<IHttpActionResult> GetEvent(int id)
        {

            Event dbEntry = await eventsRepository.Objects.Where(e => e.EventId == id).FirstOrDefaultAsync();
            if (dbEntry == null)
            {
                return NotFound();
            }
            var res = new EventViewModel
            {
                EventId = dbEntry.EventId,
                UserId = dbEntry.UserId,
                Latitude = dbEntry.Latitude,
                Longitude = dbEntry.Longitude,
                Description = dbEntry.Description,
                EventDate = dbEntry.EventDate,
                LastComments = new CommentViewModel[0],
                Photos = new SavedFileViewModel[0]
            };
            return Ok(res);
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
        [Authorize]
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
                UserId = CurrentUser.UserId,
                Latitude = model.Latitude,
                Longitude =  model.Longitude,
                Description = model.Description,
                EventDate = model.EventDate,
                DateCreate = DateTime.Now
            };
            ev = await eventsRepository.SaveInstance(ev);
            var gcmClient = new GCMClient();
            var rids = await gcmRepo.Objects.Select(r => r.RegId).ToArrayAsync();
            //await gcmClient.SendNotification(rids, new { Code = "NEW_EVENT", EventId = ev.EventId } as Object);
            GlobalHost.ConnectionManager.GetHubContext<EventsHub>().Clients.All.broadcastNewEvent(ev.EventId.ToString());
            return CreatedAtRoute("DefaultApi", new { id = ev.EventId }, ev);
        }

        // DELETE api/Events/5
        [Authorize]
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