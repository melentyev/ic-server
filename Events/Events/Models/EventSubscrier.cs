using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Models
{
    public class EventSubscrier
    {
        public int UserId { get; set; }
        public int EventId { get; set; }
        public DateTime Date { get; set; }
        //public ApplicationUser User { get; set; }
        public virtual Event Event { get; set; }
        public bool Active { get; set; }
    }
}