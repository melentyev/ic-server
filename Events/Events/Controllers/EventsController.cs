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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        private IDataRepository dataRepo;
        private const int getEventsMaxCount = 200;
        private AppUserManager userManager;
        //private ICommentsRepository commentsRepository;

        public EventsController(
            IEventsRepository pEventsRepo,
            IPhotosRepository pPhotosRepo, 
            IGcmRegIdsRepository pGcmRepo, 
            ICommentsRepository pCommentsRepo,
            IEventSubscribersRepository pEventSubsRepo,
            IDataRepository pDataRepo
            )
        {
            eventsRepository = pEventsRepo;
            photosRepo = pPhotosRepo;
            gcmRepo = pGcmRepo;
            commentsRepo = pCommentsRepo;
            eventSubsRepo = pEventSubsRepo;
            dataRepo = pDataRepo;
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
            var query = dataRepo.Events;
            query = query.OrderByDescending(e => e.EventId);
            query = offset == null ? query : query.Skip(offset.Value);
            query = query.Take(Math.Min(count, getEventsMaxCount));
            if (new string[] {plat, plng, prad}.All(e => e != null)) 
            {
                DbGeography selectionCenter = DbGeography.FromText(String.Format("POINT({0} {1})", plat, plng));
                double rad = Double.Parse(prad);

                query = query.Where(e => e.Location.Distance(selectionCenter) < rad);
            }
            if (new string[] {north, south, west, east}.All(e => e != null))
            {
                query = query.Where(e =>   e.Location.Latitude <= Double.Parse(north) 
                                && e.Location.Latitude >= Double.Parse(south)
                                && e.Location.Longitude <= Double.Parse(west)
                                && e.Location.Longitude >= Double.Parse(east));                
            }
            var res = await dataRepo.GetEventsWithCommentsAndPhotos(query);
            return Ok(res);
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
            res.User = new UserProfileViewModel(user, photo, null);
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
            string locCaption;
            using (var client = new HttpClient()) {   
                var query = String.Format("http://maps.googleapis.com/maps/api/geocode/json?latlng={0},{1}", model.Latitude, model.Longitude);
                var response = await client.GetAsync(query);
                var data = await response.Content.ReadAsStringAsync();
                try 
                {
                    locCaption = (JObject.Parse(data))["results"][0]["formatted_address"].Value<string>();
                }
                catch
                {
                    locCaption = "unknown";
                }
                
            }
            var ev = new Event
            {
                UserId = CurrentUser.UserId,
                Location = DbGeography.FromText(String.Format("POINT({1} {0})", model.Latitude, model.Longitude)),
                LocationCaption = locCaption,
                Description = model.Description,
                EventDate = model.EventDate,
                DateCreate = DateTime.UtcNow
            };
            ev = await eventsRepository.SaveInstance(ev);
            await Task.WhenAll(model.PhotoIds.Select(fid => photosRepo.SaveInstance(new Photo {
                UserId = ev.UserId, 
                AlbumId = 0, 
                EntityType = PhotoEntityTypes.Event,
                EntityId = ev.EventId,
                UserFileId = fid})));
            
            await dataRepo.Database.ExecuteSqlCommandAsync(
                "UPDATE dbo.UserFiles SET State=@p0 WHERE UserFileId IN (@p1)", 
                UserFileState.Assigned,
                String.Join(",", model.PhotoIds.ToArray()));
            
            
            var rids = await gcmRepo.Objects.Select(r => r.RegId).ToArrayAsync();

            var gcmClient = new GCMClient();
            //await gcmClient.SendNotification(rids, new { Code = "NEW_EVENT", EventId = ev.EventId } as Object);
            GlobalHost.ConnectionManager.GetHubContext<EventsHub>().Clients.All.broadcastNewEvent(ev.EventId.ToString());
            return Ok(ev.EventId);
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