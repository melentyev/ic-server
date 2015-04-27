using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Models
{
    public class Subscription
    {

        [Key, Column(Order = 0)]
        public int SubscribtionId { get; set; }
        public int? SubscriberId { get; set; }
        public int? SubscribedToId { get; set; }
        public virtual ApplicationUser Subscriber { get; set; }
        public virtual ApplicationUser SubscribedTo { get; set; }
        public Relationship Relationship { get; set; }
    }
    public enum Relationship { Unfollow, Follow, Friend, BadSubscription }
}