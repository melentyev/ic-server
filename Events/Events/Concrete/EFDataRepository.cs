using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using Events.Infrastructure;
using Events.Abstract;
using Events.Models;

namespace Events.Concrete
{
    public class EFDataRepository : IDataRepository
    {
        protected ApplicationDbContext context = new ApplicationDbContext();
        public IQueryable<Event> Events { get { return context.Events; } }
        public IQueryable<Comment> Comments { get { return context.Comments; } }
        public IQueryable<Photo> Photos { get { return context.Photos; } }
        public IQueryable<UserFile> UserFiles { get { return context.UserFiles; } }
        public async Task<IEnumerable<EventViewModel>> GetEventsWithCommentsAndPhotos(IQueryable<Event> query)
        {
            query = query.Include(e => e.User);

            var result = await query.ToArrayAsync();
            var eventsIds = result.Select(e => e.EventId);
            var comments = await Comments.Where(c => c.EntityType == CommentEntityTypes.Event && eventsIds.Contains(c.EntityId))
                .ToListAsync();
            var photos = await Photos.Where(p => p.EntityType == PhotoEntityTypes.Event && eventsIds.Contains(p.EntityId))
                .ToListAsync();
            var photosMap = photos.ToDictionary(p => p.UserFileId);
            var files = await UserFiles.Where(f => photosMap.Keys.Contains(f.UserFileId)).ToListAsync();
            var photoViewModelMap = files
                .Select(f => new { EntityId = photosMap[f.UserFileId].EntityId, ViewModel =  new PhotoViewModel 
                { 
                    Url = "/api/Endpoints/GetFile/" + f.UserFileId, 
                    UserId = f.UserId, 
                    Likes = Enumerable.Empty<SimpleUserProfileViewModel>(),
                    LikesCount = 0,
                    AlbumId = 0
                }})
                .GroupBy(p => p.EntityId)
                .ToDictionary(g => g.Key, g => g.Select(p => p.ViewModel).ToArray());
            var commentViewModelMap = comments
                .Select(c => Tuple.Create(c.EntityId, new CommentViewModel(c, null, null) ) )
                .GroupBy(c => c.Item1)
                .ToDictionary(g => g.Key, g => g.Select(t => t.Item2).ToArray()); ;
            return result.Select(e => 
            {
                return new EventViewModel(e, null,
                    commentViewModelMap.ContainsKey(e.EventId) ? commentViewModelMap[e.EventId] : null, 
                    photoViewModelMap.ContainsKey(e.EventId) ? photoViewModelMap[e.EventId] : null,
                    null);
            });
        }
    }
}