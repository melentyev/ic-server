using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Events.Models
{
    public class AddEventBindingModel
    {
        [Required]
        public string Latitude { get; set; }
        [Required]
        public string Longitude { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
    }
    public class AddCommentBindingModel
    {
        [Required]
        public int EntityId { get; set; }
        [Required]
        public string Text { get; set; }
    }
}