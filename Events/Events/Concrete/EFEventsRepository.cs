﻿using System.Linq;
using Events.Abstract;
using Events.Models;
using System.Threading.Tasks;

namespace Events.Concrete
{
    public class EFEventsRepository :  Events.Concrete.EFRepository<Event>, IEventsRepository
    {
        public EFEventsRepository() : base ("EventId") {}

        public virtual Task<Event> FindAsync(params object[] k)
        {
            return context.Set<Event>().FindAsync(k);
        }

        public Task Delete(int id)
        {
            var ev = new Event { EventId = id };
            context.Events.Attach(ev);
            context.Events.Remove(ev);
            return context.SaveChangesAsync();
        }
    }
}
