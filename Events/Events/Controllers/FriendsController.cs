using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;

using Events.Infrastructure;
using Events.Models;
using Events.Abstract;
using Events.Filters;

namespace Events.Controllers
{
    [Authorize]
    [RoutePrefix("api/Friends")]
    public class FriendsController : ApplicationApiController
    {
        private ISubscribeRepository subscribeRepository;
        public FriendsController(ISubscribeRepository subRepo)
        {
            subscribeRepository = subRepo;
        }
        // GET api/Friends

        //public IQueryable<Subscription> GetFriends()
        //{
        //    return subscribeRepository.Objects;
        //}

        // GET api/Friends/f
        [Route("/{puserId}/{param}")]
        [ResponseType(typeof(IQueryable<Subscription>))]
        public IHttpActionResult GetFriends(string puserId, string param)
        {
            var userId = CurrentUser.UserId;
            if (puserId != "my") 
            {
                if(!Int32.TryParse(puserId, out userId) ) 
                {
                    return BadRequest("/api/friends/my/{type} or /api/friends/{userId}/{type}");
                }
            }
            var friends = subscribeRepository.Objects;
            switch (param)
            {
                case "f":
                    friends = subscribeRepository.Objects.Where(e => (e.SubscribedTo == CurrentUser.UserId && e.Relationship == Subscription.relationship.follower));
                    break;
                case "s":
                    friends = subscribeRepository.Objects.Where(e => (e.Subscriber == CurrentUser.UserId && e.Relationship == Subscription.relationship.following));
                    break;
                case "m":
                    friends = subscribeRepository.Objects.Where(e => (e.Subscriber == CurrentUser.UserId && e.Relationship == Subscription.relationship.friend));
                    break;
                case "mf":
                    friends = subscribeRepository.Objects.Where
                        (e => (e.SubscribedTo == CurrentUser.UserId && (e.Relationship == Subscription.relationship.follower || e.Relationship == Subscription.relationship.friend)));
                    break;
                case "ms":
                    friends = subscribeRepository.Objects.Where
                        (e => (e.Subscriber == CurrentUser.UserId && (e.Relationship == Subscription.relationship.following || e.Relationship == Subscription.relationship.friend)));
                    break;
                default:
                    return BadRequest("incorrect input");
            }
            return Ok(@friends);
        }

        // POST api/Friends/Follow
        [Route("Follow/{id}")]
        [ResponseType(typeof(Subscription))]
        [CheckModelForNull]
        public async Task<IHttpActionResult> PostFollow(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var friend = await subscribeRepository.Objects.Where(e => (e.SubscribedTo == CurrentUser.UserId && e.Subscriber == id)).FirstOrDefaultAsync();
            var i = await subscribeRepository.Objects.Where(e => (e.Subscriber == CurrentUser.UserId && e.SubscribedTo == id)).FirstOrDefaultAsync();
            if (i == null)
            {
                var sub = new Subscription
                {
                    Subscriber = CurrentUser.UserId,
                    SubscribedTo = id,
                };
                if (friend == null)
                {
                    sub.Relationship = Subscription.relationship.following;
                    await subscribeRepository.SaveInstance(sub);
                }
                else
                {
                    sub.Relationship = Subscription.relationship.friend;
                    friend.Relationship = Subscription.relationship.friend;
                    await subscribeRepository.SaveInstance(sub);
                    await subscribeRepository.SaveInstance(friend);
                }
            }
            else
            {
                if (friend == null)
                {
                    i.Relationship = Subscription.relationship.following;
                    await subscribeRepository.SaveInstance(i);
                }
                else
                {
                    i.Relationship = Subscription.relationship.friend;
                    friend.Relationship = Subscription.relationship.friend;
                    await subscribeRepository.SaveInstance(i);
                    await subscribeRepository.SaveInstance(friend);
                }
            }
            return Ok();
        }
        // POST api/Friends/Unfollow
        [Route("Unfollow/{id}")]
        [ResponseType(typeof(Subscription))]
        [CheckModelForNull]
        public async Task<IHttpActionResult> PostUnfollow(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var friend = await subscribeRepository.Objects.Where(e => (e.SubscribedTo == CurrentUser.UserId && e.Subscriber == id)).FirstOrDefaultAsync();
            var i = await subscribeRepository.Objects.Where(e => (e.Subscriber == CurrentUser.UserId && e.SubscribedTo == id)).FirstOrDefaultAsync();
            if (i == null)
            {
                return BadRequest("You not subscribe on this user");
            }
            else
            {
                if (friend == null)
                {
                    i.Relationship = Subscription.relationship.unfollow;
                    await subscribeRepository.SaveInstance(i);
                }
                else
                {
                    i.Relationship = Subscription.relationship.unfollow;
                    friend.Relationship = Subscription.relationship.following;
                    await subscribeRepository.SaveInstance(i);
                    await subscribeRepository.SaveInstance(friend);
                }
            }
            return Ok();
        }

        //// POST api/Friends/5
        //[ResponseType(typeof(Subscription))]
        //[CheckModelForNull]
        //public async Task<IHttpActionResult> PostEvent(AddSubscribeBindingModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("asdadasd");
        //    }
        //    var ev = new Subscription
        //    {
        //        Subscriber = CurrentUser.UserId,
        //        SubscribedTo = model.SubscribedTo,
        //        Relationship = Subscription.relationship.friend
        //    };
        //    await subscribeRepository.SaveInstance(ev);

        //    return CreatedAtRoute("DefaultApi", new { id = ev.SubscribtionId }, ev);
        //}
    }
}

