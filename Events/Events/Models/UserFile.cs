using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Models
{
    public class UserFile
    {
        [Key]
        public int UserFileId{ get; set; }
        public int UserId { get; set; }
        public string FilePath { get; set; }
        public int FileSize { get; set; }
        public UserFileState State { get; set; }
        public DateTime DateCreate { get; set; }
        public int Hash { get; set; }
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