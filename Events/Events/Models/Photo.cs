using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Models
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }
        public int UserId { get; set; }
        public int AlbumId { get; set; }
        public int UserFileId { get; set; }
        public UserFile UserFile { get; set; }
        public ApplicationUser User { get; set; }
    }
}