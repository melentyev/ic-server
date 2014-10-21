using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Events.Models
{
    public class Like
    {
        public int LikeId { get; set; }
        public int UserId { get; set; }

        public LikeEntityTypes EntityType { get; set; }
        public int EntityId { get; set; }
        public DateTime Date { get; set; }
    }
    public enum LikeEntityTypes 
    {
        Photo,
        Event,
        Comment
    }
}