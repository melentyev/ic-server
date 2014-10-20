using System;
using System.Linq;
using Events.Models;
using System.Threading.Tasks;

namespace Events.Abstract
{
    public interface ISubscribeRepository : IDisposable
    {
        IQueryable<Subscription> Objects { get; }
        Task<Subscription> FindAsync(params object[] k);
        Task<Subscription> SaveInstance(Subscription subscription);
    }
}