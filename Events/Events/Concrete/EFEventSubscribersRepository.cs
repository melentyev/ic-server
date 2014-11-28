using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

using Events.Infrastructure;
using Events.Models;

using Events.Abstract;

namespace Events.Concrete
{
    public class EFEventSubscribersRepository : IEventSubscribersRepository
    {
        protected ApplicationDbContext context = new ApplicationDbContext();
        public IQueryable<EventSubscrier> Objects { get { return context.EventSubscriers; } }
        public Task ToggleSubscription(int userId, int eventId)
        {
            var inst = Objects.Where(s => s.EventId == eventId && s.UserId == userId).FirstOrDefault();
            if (inst != null) 
            {
                inst.Active = !inst.Active;
            }
            else
            {
                context.EventSubscriers.Add(new EventSubscrier { EventId = eventId, UserId = userId, Active = true, Date = DateTime.UtcNow });
            }
            return context.SaveChangesAsync();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}