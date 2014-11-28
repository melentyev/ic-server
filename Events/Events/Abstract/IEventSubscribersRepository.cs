using System;
using System.Linq;
using System.Threading.Tasks;

using Events.Models;

namespace Events.Abstract
{
    public interface IEventSubscribersRepository : IDisposable
    {
        IQueryable<EventSubscrier> Objects { get; }
        Task ToggleSubscription(int userId, int eventId);
        //Task<Event> SaveInstance(Event ev);
    }
}