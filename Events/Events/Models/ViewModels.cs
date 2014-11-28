﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Events.Models
{
    public class EventViewModel
    {
        public EventViewModel (Event e, 
            Photo userPhoto,
            IEnumerable<CommentViewModel> comments, 
            IEnumerable<PhotoViewModel> photos,
            IEnumerable<SimpleUserProfileViewModel> likes)
        {
            EventId = e.EventId;
            User = e.User != null ? new UserProfileViewModel(e.User, userPhoto) : null;
            Latitude = e.Location == null ? null : e.Location.Latitude.ToString();
            Longitude = e.Location == null ? null : e.Location.Longitude.ToString();
            Description = e.Description;
            EventDate = e.EventDate;
            DateCreate = e.DateCreate;
            LastComments = comments;
            Photos = photos;
            LastLikes = likes;
        }
        public int EventId { get; set; }
        public UserProfileViewModel User { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime DateCreate { get; set; }
        public IEnumerable<CommentViewModel> LastComments { get; set; } //optional
        public IEnumerable<PhotoViewModel> Photos { get; set; } //optional
        public IEnumerable<SimpleUserProfileViewModel> LastLikes { get; set; } //optional
    }

    public class EventCategoryViewModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
    
    public class CommentViewModel
    {
        public CommentViewModel(Comment c, SimpleUserProfileViewModel user, IEnumerable<SimpleUserProfileViewModel> paramLikes)
        {
            CommentId = c.CommentId;
            User = user;
            Text = c.Text;
            DateCreate = c.DateCreate;
            Likes = paramLikes;
            LikesCount = c.LikesCount;
        }
        public int CommentId { get; set; }
        public SimpleUserProfileViewModel User { get; set; }
        public string Text { get; set; }
        public DateTime DateCreate { get; set; }
        public IEnumerable<SimpleUserProfileViewModel> Likes { get; set; }
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
        public PhotoViewModel(Photo p)
        {
            UserId = p.User.Id;
            AlbumId = p.AlbumId;
            Url = p.UserFile.GetFullUrl();
        }
        public string Url { get; set; }
        public int UserId { get; set; }
        public int AlbumId { get; set; }
        public IEnumerable<SimpleUserProfileViewModel> Likes { get; set; }
        public int LikesCount { get; set; }

    }
    public class UserProfileViewModel
    {
        public UserProfileViewModel(ApplicationUser u, Photo p)
        {
            UserId = u.Id;
            UserName = u.UserName;
            Photo = p == null ? null : new PhotoViewModel(p);
        }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public PhotoViewModel Photo { get; set; }
    }
    public class SimpleUserProfileViewModel
    {
        public SimpleUserProfileViewModel(ApplicationUser user) 
        {
            UserId = user.Id;
            UserName = user.UserName;
            SmallPhotoUrl = ""; //user.GetPhoto().UserFile.GetFullUrl();
        }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string SmallPhotoUrl { get; set; }

    }


}