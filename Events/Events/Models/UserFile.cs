using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Events.Models
{
    public class UserFile
    {
        public int UserFileId { get; set; }
        public int UserId { get; set; }
        public string FilePath { get; set; }
        public int FileSize { get; set; }
        public UserFileState State { get; set; }
        public DateTime DateCreate { get; set; }        
        public int ServerId { get; set; }
        public virtual Server Server { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string GetFullUrl()
        {
            return /*"http://" + Server.Domain +*/  "/Uploads/" + FilePath;
        }
    }
    public enum UserFileState
    {
        JustUploaded,
        Saved,
        Assigned,
        Unused,
        Removed
    }
}