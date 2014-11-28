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
using System.Data.Entity.Spatial;

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
        private IPhotosRepository photosRepo;
        private ICommentsRepository commentsRepo;
        private IEventSubscribersRepository eventSubsRepo;
        private const int getEventsMaxCount = 200;
        private AppUserManager userManager;
        //private ICommentsRepository commentsRepository;

        public EventsController(
            IEventsRepository pEventsRepo,
            IPhotosRepository pPhotosRepo, 
            IGcmRegIdsRepository pGcmRepo, 
            ICommentsRepository pCommentsRepo,
            IEventSubscribersRepository pEventSubsRepo
            )
        {
            eventsRepository = pEventsRepo;
            photosRepo = pPhotosRepo;
            gcmRepo = pGcmRepo;
            commentsRepo = pCommentsRepo;
            eventSubsRepo = pEventSubsRepo;
            userManager = Startup.UserManagerFactory();
        }


        // GET api/Events
        [CheckOptionalDoubleArg("south", "north", "west", "east", "plat", "plng", "prad")]
        [CheckOptionalIntArg("offset")]
        [ResponseType(typeof(IEnumerable<EventViewModel>))]
        public async Task<IHttpActionResult> GetEvents(int? offset = null, int count = 10, 
            string south = null, 
            string north = null, 
            string west = null, 
            string east = null,
            string plat = null,
            string plng = null,
            string prad = null
            )
        {
            var query = eventsRepository.Objects.Include(e => e.User);
            query = offset == null ? query : query.Skip(offset.Value);
            query = query.Take(Math.Min(count, getEventsMaxCount));
            if (prad != null) 
            {
                DbGeography p0 = DbGeography.FromText(String.Format("POINT({0} {1})", plat, plng));
                double rad;
                if (!Double.TryParse(prad, out rad))
                {
                    return BadRequest(Messages.Get("INVALID_DOUBLE", d => d + " (" + rad + ")"));
                }
                query = query.Where(e => e.Location.Distance(p0) < rad);
            }
            if (south != null)
            {
                query = query.Where(e =>   e.Location.Latitude <= Double.Parse(north) 
                                && e.Location.Latitude >= Double.Parse(south)
                                && e.Location.Longitude <= Double.Parse(west)
                                && e.Location.Longitude >= Double.Parse(east));                
            }
            
            var result = query.ToArray();
            var eventsIds = result.Select(e => e.EventId);
            var comments = commentsRepo.Objects.Where(c => c.EntityType == EntityTypes.Event && eventsIds.Contains(c.EntityId))
                .ToList();
            var eventComments = 
                comments
                    .GroupBy(c => c.EntityId)
                    .ToDictionary(group => group.Key, group => group.ToArray());

            return Ok(result.Select(e => 
            {
                IEnumerable<CommentViewModel> coms = null; 
                if (eventComments.ContainsKey(e.EventId) )
                { 
                    coms = eventComments[e.EventId].Select(c => new CommentViewModel(c, null, null));
                }
                return new EventViewModel(e, null, coms, null, null);
            }));
        }

        // GET api/Events/5
        [ResponseType(typeof(EventViewModel))]
        public async Task<IHttpActionResult> GetEvent(int id)
        {

            Event dbEntry = await eventsRepository.Objects.Where(e => e.EventId == id)
                .Include(e => e.User)
                .FirstOrDefaultAsync();
            if (dbEntry == null)
            {
                return NotFound();
            }
            var res = new EventViewModel(dbEntry, null, null, null, null);

            var user = await userManager.FindByIdAsync(dbEntry.UserId);
            var photo = await photosRepo.Objects.Where(p => user.PhotoId == p.PhotoId).FirstOrDefaultAsync();
            res.User = new UserProfileViewModel(user, photo);
            res.LastComments = new CommentViewModel[0];
            res.Photos = new PhotoViewModel[0];
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
                Location = DbGeography.FromText(String.Format("POINT({1} {0})", model.Latitude, model.Longitude)),
                Description = model.Description,
                EventDate = model.EventDate,
                DateCreate = DateTime.UtcNow
            };
            ev = await eventsRepository.SaveInstance(ev);
            
            var rids = await gcmRepo.Objects.Select(r => r.RegId).ToArrayAsync();

            var gcmClient = new GCMClient();
            //await gcmClient.SendNotification(rids, new { Code = "NEW_EVENT", EventId = ev.EventId } as Object);
            GlobalHost.ConnectionManager.GetHubContext<EventsHub>().Clients.All.broadcastNewEvent(ev.EventId.ToString());
            return Ok(new { EventId = ev.EventId });
        }

        [Route("api/Events/Subscribe/{id}")]
        public async Task<IHttpActionResult> Subscribe(int id)
        {
            var ev = await eventsRepository.FindAsync(id);
            if (ev == null) 
            {
                return BadRequest(Messages.Get("OBJECT_NOT_FOUND", d => d + " Event " + id.ToString() ) );
            }
            await eventSubsRepo.ToggleSubscription(CurrentUser.UserId, id);
            return Ok();
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