using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Events.Models;

namespace Events.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<Event> Events { get; set; }
//        public System.Data.Entity.DbSet<Comment> Comment { get; set; }
//        public System.Data.Entity.DbSet<Subscription> Subscription { get; set; }
    }
}