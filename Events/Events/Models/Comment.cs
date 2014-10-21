using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Events.Models
{
    public enum EntityTypes 
    {
        Event
    }  
    public class Comment
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public EntityTypes EntityType { get; set; }
        public int EntityId { get; set; }
        public string Text { get; set; }
        public DateTime DateCreate { get; set; }
        public int LikesCount { get; set; }
    }
}