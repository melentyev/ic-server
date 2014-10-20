using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Events.Models
{
    public class EventViewModel
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public IEnumerable<CommentViewModel> LastComments { get; set; } //optional
        public IEnumerable<SavedFileViewModel> Photos { get; set; } //optional
    }

    public class EventCategoryViewModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
    
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime DateCreate { get; set; }
    }
    public class SavedFileViewModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int FileSize { get; set; }
    }
}