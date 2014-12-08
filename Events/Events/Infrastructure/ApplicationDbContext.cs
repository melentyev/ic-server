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
            Database.Log = (msg => {                
                myFileLog.Write(msg);
                myFileLog.Close();
                m_myFileLog = null;
            });
        }
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<ApplicationDbContext>());
            builder.Entity<Event>().HasKey(e => e.EventId);
            builder.Entity<EventSubscrier>().HasKey(q => new { q.EventId, q.UserId });
            builder.Entity<EventSubscrier>()
                .HasRequired(e => e.Event)
                .WithMany()
                .HasForeignKey(e => e.EventId);
            /*builder.Entity<Photo>()
                .HasRequired(u => u.Photo)
                .WithRequiredPrincipal(u => u.)
                .HasOptional(u => u.Photo)
                .*/
                
            /*builder.Entity<Event>()
                .HasMany(c => c)
                .WithMany()                 // Note the empty WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("CountryId");
                    x.MapRightKey("CurrencyId");
                    x.ToTable("CountryCurrencyMapping");
                });
            */
            base.OnModelCreating(builder);
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
        public System.Data.Entity.DbSet<EventSubscrier> EventSubscriers { get; set; }
        public System.Data.Entity.DbSet<UserFile> UserFile { get; set; }
    }
}