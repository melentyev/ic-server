using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Events.Models;
using System.Security.Claims;

namespace Events.Infrastructure
{
    public abstract class ApplicationApiController : ApiController
    {
        public AppClaimsPrincipal CurrentUser
        {
            get { return new AppClaimsPrincipal((ClaimsPrincipal)this.User); }
        }

        public ApplicationApiController()
        {

        }
    }
}