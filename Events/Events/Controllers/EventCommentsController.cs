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
using Events.Infrastructure;

namespace Events.Controllers
{
    public class EventCommentsController : ApplicationApiController
    {
        private IEventsRepository eventsRepository;
        private ICommentsRepository commentsRepository;
        public EventCommentsController(IEventsRepository evRepo, ICommentsRepository comRepo)
        {
            eventsRepository = evRepo;
            commentsRepository = comRepo;
        }

        // GET api/EventComments
        [Authorize]
        public IQueryable<Event> GetEventComments()
        {
            return eventsRepository.Objects;
        }

        [Authorize]
        [ResponseType(typeof(Comment))]
        [CheckModelForNull]
        public async Task<IHttpActionResult> PostEventComment(AddCommentBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = new Comment
            {
                UserId = CurrentUser.UserId,
                EntityId = model.EntityId,
                EntityType = EntityTypes.Event,
                Text = model.Text,
                DateCreate = DateTime.Now
            };
            await commentsRepository.SaveInstance(comment);

            return CreatedAtRoute("DefaultApi", new { id = comment.CommentId }, comment);
        }
    }
}
