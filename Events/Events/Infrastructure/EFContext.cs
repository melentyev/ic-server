using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Events.Infrastructure
{
    public static class EFContext
    {
        //public ApplicationDbContext context = null;
        public static ApplicationDbContext Get()
        {
            return new ApplicationDbContext();
        }
    }
}