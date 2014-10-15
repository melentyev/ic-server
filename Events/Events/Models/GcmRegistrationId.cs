using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Events.Models
{
    public class GcmRegistrationId
    {
        [Key]
        public string RegId { get; set; }
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}