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
        public DateTime EventDate { get; set; }
    }
    public class AddCommentBindingModel
    {
        [Required]
        public int EntityId { get; set; }
        [Required]
        public string Text { get; set; }
    }

    public class AddSubscribeBindingModel
    {
        [Required]
        public int SubscribedTo { get; set; }
    }
    public class SaveFileBindingModel
    {
        public List<int> FileId { get; set; }
        public int Hash { get; set; }
    }
}