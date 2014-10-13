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
        public enum relationship {unfollow, follower, following, friend }
        [Key]
        public int SubscribtionId { get; set; }
        public int Subscriber { get; set; }
        public int SubscribedTo { get; set; }
        public relationship Relationship { get; set; }
    }
}