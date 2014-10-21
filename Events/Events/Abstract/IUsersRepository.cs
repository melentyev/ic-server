using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Events.Models;

namespace Events.Abstract
{
    public interface IUsersRepository : IDisposable
    {
        IQueryable<ApplicationUser> Objects { get; }
        Task<ApplicationUser> FindAsync(params object[] k);
        Task<ApplicationUser> SaveInstance(ApplicationUser regId);
    }
}
