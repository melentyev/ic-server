using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Models;

namespace Events.Abstract
{
    public interface IGcmRegIdsRepository : IDisposable
    {
        IQueryable<GcmRegistrationId> Objects { get; }
        Task<GcmRegistrationId> FindAsync(params object[] k);
        Task SaveInstance(GcmRegistrationId regId);
    }
}
