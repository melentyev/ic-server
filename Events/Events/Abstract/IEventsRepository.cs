using System;
using System.Linq;
using Events.Models;
using System.Threading.Tasks;

namespace Events.Abstract
{
    public interface IEventsRepository : IDisposable
    {
        IQueryable<Event> Objects { get; }
        Task<Event> FindAsync(params object[] k);
        Task SaveInstance(Event ev);
    }
}
