using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using Events.Models;

namespace Events.Abstract
{
    public interface IDataRepository
    {
        Database Database { get; }
        IQueryable<Event> Events { get; }
        IQueryable<Comment> Comments { get; }
        IQueryable<Photo> Photos { get;  }
        IQueryable<UserFile> UserFiles { get; }
        IQueryable<ApplicationUser> Users { get; }
        Task<IEnumerable<EventViewModel>> GetEventsWithCommentsAndPhotos(IQueryable<Event> query);
        Task SaveChangesAsync();
    }
}