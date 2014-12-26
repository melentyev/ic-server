using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

using Events.Models;

namespace Events.Abstract
{
    public interface IDataRepository
    {
        IQueryable<Event> Events { get; }
        IQueryable<Comment> Comments { get; }
        IQueryable<Photo> Photos { get;  }
        Task<IEnumerable<EventViewModel>> GetEventsWithCommentsAndPhotos(IQueryable<Event> query);
    }
}