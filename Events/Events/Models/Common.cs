﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Events.Infrastructure;

namespace Events.Models
{
    public class ApplicationUser :
        IdentityUser<int, AppUserLogin, AppUserRole, AppUserClaim>,
        IUser<int>
    {

        public override string Email { get; set; }
    }
    public class AppUserLogin : IdentityUserLogin<int> { }

    public class AppUserRole : IdentityUserRole<int> { }

    public class AppUserClaim : IdentityUserClaim<int> { }

    public class AppRole : IdentityRole<int, AppUserRole> { }
    public class AppClaimsPrincipal : ClaimsPrincipal
    {
        public AppClaimsPrincipal(ClaimsPrincipal principal)
            : base(principal)
        { }

        public int UserId
        {
            get { return int.Parse(this.FindFirst(ClaimTypes.Sid).Value); }
        }
    }

    public interface IAppUserStore : IUserStore<ApplicationUser, int> {}

    public class AppUserStore :
        UserStore<ApplicationUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>,
        IAppUserStore
    {
        public AppUserStore()
            : base(new ApplicationDbContext())
        {

        }

        public AppUserStore(ApplicationDbContext context)
            : base(context)
        {

        }
    }
    public class AppUserManager : UserManager<ApplicationUser, int>
    {
        public AppUserManager(IAppUserStore store)
            : base(store)
        {

        }
    }
}