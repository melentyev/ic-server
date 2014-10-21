using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Events.Models
{
    public class EventViewModel
    {
        public EventViewModel (Event e)
        {
            EventId = e.EventId;
            Latitude = e.Latitude;
            Longitude = e.Longitude;
            Description = e.Description;
            EventDate = e.EventDate;
        }
        public int EventId { get; set; }
        public UserProfileViewModel User { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public IEnumerable<CommentViewModel> LastComments { get; set; } //optional
        public IEnumerable<PhotoViewModel> Photos { get; set; } //optional
    }

    public class EventCategoryViewModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Like> Likes { get; set; }
        public int LikesCount { get; set; }
    }
    
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime DateCreate { get; set; }
        public IEnumerable<Like> Likes { get; set; }
        public int LikesCount { get; set; }
    }
    public class SavedFileViewModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int FileSize { get; set; }
    }
    public class PhotoViewModel 
    {
        public string Url { get; set; }
        public int UserId { get; set; }
        public IEnumerable<Like> Likes { get; set; }
        public int LikesCount { get; set; }

    }
    public class UserProfileViewModel
    {
        public UserProfileViewModel(ApplicationUser u)
        {
            UserName = u.UserName;
        }
        public string UserName { get; set; }
        public PhotoViewModel Photo { get; set; }
    }
}