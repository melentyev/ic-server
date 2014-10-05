using System;
using System.Linq;
using Events.Models;
using System.Threading.Tasks;

namespace Events.Abstract
{
    public interface ICommentsRepository : IDisposable
    {
        IQueryable<Comment> Objects { get; }
        Task<Comment> FindAsync(params object[] k);
        Task SaveInstance(Comment comment);
    }
}