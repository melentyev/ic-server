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
        private IPhotosRepository photosRepo;
        public FriendsController(
            IPhotosRepository paramPhotosRepo, 
            ISubscribeRepository subRepo)
        {
            photosRepo = paramPhotosRepo;
            subscribeRepository = subRepo;
        }
        // GET api/Friends

        //public IQueryable<Subscription> GetFriends()
        //{
        //    return subscribeRepository.Objects;
        //}

        // GET api/Friends/f
        [Route("List/{puserId}/{param}")] 
        [ResponseType(typeof(UserProfileViewModel[]))]
        public async Task<IHttpActionResult> GetFriends(string puserId, string param)
        {
            var userId = CurrentUser.UserId;
            if (puserId != "my") 
            {
                if(!Int32.TryParse(puserId, out userId) ) 
                {
                    return BadRequest("/api/Friends/List/my/{type} or /api/Friends/List/{userId}/{type}");
                }
            }
            var rels = subscribeRepository.Objects;
            var variants = new Dictionary<string, Func<IQueryable<ApplicationUser> > >();
            variants["f"]  = () => 
                rels.Where(s => s.SubscribedToId == userId && s.Relationship == Relationship.Follow).Select(s => s.Subscriber);
            variants["s"]  = () => 
                rels.Where(s => s.SubscriberId == userId && s.Relationship == Relationship.Follow).Select(s => s.SubscribedTo);
            variants["m"] = () =>
                Queryable.Concat(
                    rels.Where(
                        s =>
                            s.SubscriberId == userId &&
                            s.Relationship == Relationship.Friend).Select(s => s.SubscribedTo),
                    rels.Where(
                        s =>
                            s.SubscribedToId == userId &&
                            s.Relationship == Relationship.Friend).Select(s => s.Subscriber));
            //variants["mf"] = () => rels
            //    .Where(s => (s.SubscribedToId == userId && (s.Relationship == Relationship.Follower || s.Relationship == Relationship.Friend)))
            //    .Select(s => s.Subscriber);
            //variants["ms"] = () => rels
            //    .Where(s => (s.SubscriberId == userId && (s.Relationship == Relationship.Following || s.Relationship == Relationship.Friend)))
            //    .Select(s => s.SubscribedTo);
            if(!variants.ContainsKey(param) ) 
            {
                return BadRequest("incorrect input");
            }
            var result = await variants[param]().ToArrayAsync();

            return Ok(result.Select(u =>
            {
                var photo = photosRepo.Objects.Where(p => p.PhotoId == u.PhotoId).FirstOrDefault();
                return new UserProfileViewModel(u, photo, null);
            }).ToArray());
        }

        // POST api/Friends/Follow
        [Route("Follow/{id}")]
        [ResponseType(typeof(Subscription))]
        public async Task<IHttpActionResult> PostFollow(int id)
        {
            var toMe = await subscribeRepository.Objects.Where(e => (e.SubscribedToId == CurrentUser.UserId && e.SubscriberId == id && e.Relationship != Relationship.BadSubscription)).FirstOrDefaultAsync();
            var fromMe = await subscribeRepository.Objects.Where(e => (e.SubscriberId == CurrentUser.UserId && e.SubscribedToId == id && e.Relationship != Relationship.BadSubscription)).FirstOrDefaultAsync();
            if (fromMe == null)
            {
                if (toMe == null)
                {
                    var sub = new Subscription
                    {
                        SubscriberId = CurrentUser.UserId,
                        SubscribedToId = id,
                        Relationship = Relationship.Follow
                    };
                    await subscribeRepository.SaveInstance(sub);
                }
                else
                {
                    if (toMe.Relationship == Relationship.Unfollow)
                    {
                        toMe.SubscriberId = CurrentUser.UserId;
                        toMe.SubscribedToId = id;
                        toMe.Relationship = Relationship.Follow;
                    }
                    else
                    {
                        toMe.Relationship = Relationship.Friend;                        
                    }
                    await subscribeRepository.SaveInstance(toMe);
                }
            }
            else
            {
                if (toMe == null)
                {
                    if (fromMe.Relationship == Relationship.Unfollow)
                    {
                        fromMe.Relationship = Relationship.Follow;
                        await subscribeRepository.SaveInstance(fromMe);
                    }
                }
                else
                {
                    switch (fromMe.Relationship)
                    {
                            case Relationship.Follow:
                                switch (toMe.Relationship)
                                {
                                    case Relationship.Follow:
                                        fromMe.Relationship = Relationship.Friend;
                                        toMe.Relationship = Relationship.BadSubscription;
                                        break;
                                    case Relationship.Unfollow:
                                        toMe.Relationship = Relationship.BadSubscription;
                                        break;
                                    case Relationship.Friend:
                                        fromMe.Relationship = Relationship.BadSubscription;
                                        break;
                                };
                                break;
                            case Relationship.Unfollow:
                                switch (toMe.Relationship)
                                {
                                    case Relationship.Follow:
                                        fromMe.Relationship = Relationship.Friend;
                                        toMe.Relationship = Relationship.BadSubscription;
                                        break;
                                    case Relationship.Unfollow:
                                        fromMe.Relationship = Relationship.Follow;
                                        toMe.Relationship = Relationship.BadSubscription;
                                        break;
                                    case Relationship.Friend:
                                        fromMe.Relationship = Relationship.Friend;
                                        toMe.Relationship = Relationship.BadSubscription;
                                        break;
                                };
                                break;
                        case Relationship.Friend:
                                switch (toMe.Relationship)
                                {
                                    case Relationship.Follow:
                                        toMe.Relationship = Relationship.BadSubscription;
                                        break;
                                    case Relationship.Unfollow:
                                        fromMe.Relationship = Relationship.Follow;
                                        toMe.Relationship = Relationship.BadSubscription;
                                        break;
                                    case Relationship.Friend:
                                        toMe.Relationship = Relationship.BadSubscription;
                                        break;
                                };
                                break;
                    }
                    await subscribeRepository.SaveInstance(fromMe);
                    await subscribeRepository.SaveInstance(toMe);
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
            var toMe = await subscribeRepository.Objects.Where(e => (e.SubscribedToId == CurrentUser.UserId && e.SubscriberId == id && e.Relationship != Relationship.BadSubscription)).FirstOrDefaultAsync();
            var fromMe = await subscribeRepository.Objects.Where(e => (e.SubscriberId == CurrentUser.UserId && e.SubscribedToId == id && e.Relationship != Relationship.BadSubscription)).FirstOrDefaultAsync();
            if (fromMe == null)
            {
                if (toMe == null)
                {
                    return Ok("You are not subscribed to this user");
                }
                else
                {
                    if (toMe.Relationship == Relationship.Friend)
                    {
                        toMe.Relationship = Relationship.Follow;
                        await subscribeRepository.SaveInstance(toMe);
                    }
                    else
                    {
                        return Ok("You are not subscribed to this user");
                    }
                }
            }
            else
            {
                if (toMe == null)
                {
                    if (fromMe.Relationship == Relationship.Friend)
                    {
                        fromMe.SubscriberId = id;
                        fromMe.SubscribedToId = CurrentUser.UserId;
                        fromMe.Relationship = Relationship.Follow;
                    }
                    else
                    {
                        fromMe.Relationship = Relationship.Unfollow;
                    }
                    await subscribeRepository.SaveInstance(fromMe);
                }
                else
                {
                    switch (fromMe.Relationship)
                    {
                        case Relationship.Follow:
                            switch (toMe.Relationship)
                            {
                                case Relationship.Follow:
                                    fromMe.Relationship = Relationship.BadSubscription;
                                    break;
                                case Relationship.Unfollow:
                                    fromMe.Relationship = Relationship.BadSubscription;
                                    break;
                                case Relationship.Friend:
                                    fromMe.Relationship = Relationship.BadSubscription;
                                    toMe.Relationship = Relationship.Follow;
                                    break;
                            }
                            ;
                            break;
                        case Relationship.Unfollow:
                            switch (toMe.Relationship)
                            {
                                case Relationship.Follow:
                                    fromMe.Relationship = Relationship.BadSubscription;
                                    break;
                                case Relationship.Unfollow:
                                    toMe.Relationship = Relationship.BadSubscription;
                                    break;
                                case Relationship.Friend:
                                    fromMe.Relationship = Relationship.BadSubscription;
                                    toMe.Relationship = Relationship.Follow;
                                    break;
                            }
                            ;
                            break;
                        case Relationship.Friend:
                            switch (toMe.Relationship)
                            {
                                case Relationship.Follow:
                                    fromMe.Relationship = Relationship.BadSubscription;
                                    break;
                                case Relationship.Unfollow:
                                    fromMe.Relationship = Relationship.BadSubscription;
                                    break;
                                case Relationship.Friend:
                                    fromMe.Relationship = Relationship.BadSubscription;
                                    toMe.Relationship = Relationship.Follow;
                                    break;
                            }
                            ;
                            break;
                    }
                    await subscribeRepository.SaveInstance(fromMe);
                    await subscribeRepository.SaveInstance(toMe);
                }
            }
            return Ok();
        }
    }
}

