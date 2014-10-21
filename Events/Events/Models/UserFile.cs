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