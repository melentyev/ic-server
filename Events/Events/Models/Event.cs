using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Events.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public DbGeography Location { get; set; } 
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime DateCreate { get; set; }
        public int LikesCount { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<EventSubscrier> EventSubscrier { get; set; }
    }
}

