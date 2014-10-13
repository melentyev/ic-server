using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Events.Models;
using System.IO;

namespace Events.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            Database.Log = (msg => {
                //myLog.Add(msg);
                myFileLog.Write(msg);
            });
        }
        static StreamWriter myFileLog = new StreamWriter(new FileStream(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\mydblog.txt", FileMode.Append));
        static IList<string> myLog = new List<string>();
        public System.Data.Entity.DbSet<Events.Models.Event> Events { get; set; }
    }
}