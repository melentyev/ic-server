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
    [RoutePrefix("api/Friends")]
    [Authorize]
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
        [Route("{puserId}/{param}")]
        [ResponseType(typeof(UserProfileViewModel[]))]
        public async Task<IHttpActionResult> GetFriends(string puserId, string param)
        {
            var userId = CurrentUser.UserId;
            if (puserId != "my") 
            {
                if(!Int32.TryParse(puserId, out userId) ) 
                {
                    return BadRequest("/api/friends/my/{type} or /api/friends/{userId}/{type}");
                }
            }
            var rels = subscribeRepository.Objects;
            IQueryable<ApplicationUser> users; 
            var variants = new Dictionary<string, Func<IQueryable<ApplicationUser> > >();
            variants["f"]  = () => 
                rels.Where(s => s.SubscribedToId == userId && s.Relationship == Relationship.Follower).Select(s => s.Subscriber);
            variants["s"]  = () => 
                rels.Where(s => s.SubscriberId == userId && s.Relationship == Relationship.Following).Select(s => s.SubscribedTo);
            variants["m"]  = () => 
                rels.Where(s => s.SubscriberId == userId && s.Relationship == Relationship.Friend).Select(s => s.SubscribedTo);
            variants["mf"] = () => rels
                .Where(s => (s.SubscribedToId == userId && (s.Relationship == Relationship.Follower || s.Relationship == Relationship.Friend)))
                .Select(s => s.Subscriber);
            variants["ms"] = () => rels
                .Where(s => (s.SubscriberId == userId && (s.Relationship == Relationship.Following || s.Relationship == Relationship.Friend)))
                .Select(s => s.SubscribedTo);
            if(!variants.ContainsKey(param) ) 
            {
                return BadRequest("incorrect input");
            }
            var result = await variants[param]().Select(u => new UserProfileViewModel(u)).ToArrayAsync();
            return Ok(result);
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
            var friend = await subscribeRepository.Objects.Where(e => (e.SubscribedToId == CurrentUser.UserId && e.SubscriberId == id)).FirstOrDefaultAsync();
            var i = await subscribeRepository.Objects.Where(e => (e.SubscriberId == CurrentUser.UserId && e.SubscribedToId == id)).FirstOrDefaultAsync();
            if (i == null)
            {
                var sub = new Subscription
                {
                    SubscriberId = CurrentUser.UserId,
                    SubscribedToId = id,
                };
                if (friend == null)
                {
                    sub.Relationship = Relationship.Following;
                    await subscribeRepository.SaveInstance(sub);
                }
                else
                {
                    sub.Relationship = Relationship.Friend;
                    friend.Relationship = Relationship.Friend;
                    await subscribeRepository.SaveInstance(sub);
                    await subscribeRepository.SaveInstance(friend);
                }
            }
            else
            {
                if (friend == null)
                {
                    i.Relationship = Relationship.Following;
                    await subscribeRepository.SaveInstance(i);
                }
                else
                {
                    i.Relationship = Relationship.Friend;
                    friend.Relationship = Relationship.Friend;
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
            var friend = await subscribeRepository.Objects.Where(e => (e.SubscribedToId == CurrentUser.UserId && e.SubscriberId == id)).FirstOrDefaultAsync();
            var i = await subscribeRepository.Objects.Where(e => (e.SubscriberId == CurrentUser.UserId && e.SubscribedToId == id)).FirstOrDefaultAsync();
            if (i == null)
            {
                return BadRequest("You not subscribe on this user");
            }
            else
            {
                if (friend == null)
                {
                    i.Relationship = Relationship.Unfollow;
                    await subscribeRepository.SaveInstance(i);
                }
                else
                {
                    i.Relationship = Relationship.Unfollow;
                    friend.Relationship = Relationship.Following;
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

