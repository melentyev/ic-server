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
    public class FriendsController : ApplicationApiController
    {
        private ISubscribeRepository subscribeRepository;
        public FriendsController(ISubscribeRepository subRepo)
        {
            subscribeRepository = subRepo;
        }
        // GET api/Friends

        public IQueryable<Subscription> GetFriends()
        {
            return subscribeRepository.Objects;
        }

        // GET api/Friends/f
        //[ResponseType(typeof(IQueryable<Subscription>))]
        public IQueryable<Subscription> GetFriend(string parametr)
        {
            var @friends = subscribeRepository.Objects;
            switch (parametr)
            {
                case "f":
                    @friends = subscribeRepository.Objects.Where(e => (e.Subscriber == CurrentUser.UserId && e.Relationship == Subscription.relationship.follower));
                    break;
                case "s":
                    @friends = subscribeRepository.Objects.Where(e => (e.SubscribedTo == CurrentUser.UserId && e.Relationship == Subscription.relationship.following));
                    break;
                default:
                    @friends = subscribeRepository.Objects.Where(e => (e.SubscribedTo == CurrentUser.UserId && e.Relationship == Subscription.relationship.friend));
                    break;
            }
            return @friends;
        }

        // POST api/Friends/Follow
        [ResponseType(typeof(Subscription))]
        [CheckModelForNull]
        public async Task<IHttpActionResult> PostSubscribe(string parametr, AddSubscribeBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var friend = await subscribeRepository.Objects.Where(e => (e.SubscribedTo == CurrentUser.UserId && e.Subscriber == model.SubscribedTo)).FirstOrDefaultAsync();
            var i = await subscribeRepository.Objects.Where(e => (e.Subscriber == CurrentUser.UserId && e.SubscribedTo == model.SubscribedTo)).FirstOrDefaultAsync();
            if (i == null)
            {
                switch (parametr)
                {
                    case "follow":
                        var sub = new Subscription
                        {
                            Subscriber = CurrentUser.UserId,
                            SubscribedTo = model.SubscribedTo,
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
                        break;
                    case "unfollow":
                        return NotFound();
                        break;
                }
            }
            else
            {
                switch (parametr)
                {
                    case "follow":
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
                        break;
                    case "unfollow":
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
                        break;
                }
            }
            return Ok();
        }
    }
}

