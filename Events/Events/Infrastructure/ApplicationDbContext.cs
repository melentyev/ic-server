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
            /*Database.Log = (msg => {                
                myFileLog.Write(msg);
            });*/
        }
        private static StreamWriter m_myFileLog = null;
        static StreamWriter myFileLog 
        { 
            get 
            { 
                if (m_myFileLog == null) 
                {
                    m_myFileLog = new StreamWriter(new FileStream(
                        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\mydblog.txt", FileMode.Append));
                } 
                return m_myFileLog;
            } 
        }
        static IList<string> myLog = new List<string>();
        public System.Data.Entity.DbSet<Event> Events { get; set; }
        public System.Data.Entity.DbSet<Comment> Comment { get; set; }
        public System.Data.Entity.DbSet<Subscription> Subscription { get; set; }
    }
}