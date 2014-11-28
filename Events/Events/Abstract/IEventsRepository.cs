using System;
using System.Linq;
using System.Threading.Tasks;

using Events.Models;

namespace Events.Abstract
{
    public interface IEventsRepository : IDisposable
    {
        IQueryable<Event> Objects { get; }
        Task<Event> FindAsync(params object[] k);
        Task<Event> SaveInstance(Event ev);
    }
}
