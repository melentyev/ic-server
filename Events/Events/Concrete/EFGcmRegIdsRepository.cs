using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Threading.Tasks;

using Events.Abstract;
using Events.Models;
using Events.Concrete;

namespace Events.Concrete
{
    public class EFGcmRegIdsRepository : Events.Concrete.EFRepository<GcmRegistrationId>, IGcmRegIdsRepository
    {
        public EFGcmRegIdsRepository() : base("RegId") { }
        public virtual Task<GcmRegistrationId> FindAsync(params object[] k)
        {
            return context.Set<GcmRegistrationId>().FindAsync(k);
        }
    }
}