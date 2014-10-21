using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Events.Models;
using System.IO;
using System.Data.Entity;

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
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           /* modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.)
                .WithMany()                 // Note the empty WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("CountryId");
                    x.MapRightKey("CurrencyId");
                    x.ToTable("CountryCurrencyMapping");
                });
            */
            base.OnModelCreating(modelBuilder);
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
        public System.Data.Entity.DbSet<Comment> Comments { get; set; }
        public System.Data.Entity.DbSet<Subscription> Subscriptions { get; set; }
        public System.Data.Entity.DbSet<GcmRegistrationId> GcmRegistrationIds { get; set; }
    }
}