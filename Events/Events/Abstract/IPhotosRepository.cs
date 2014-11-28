using System;
using System.Linq;
using Events.Models;
using System.Threading.Tasks;

namespace Events.Abstract
{
    public interface IPhotosRepository : IDisposable
    {
        IQueryable<Photo> Objects { get; }
        Task<Photo> FindAsync(params object[] k);
        Task<Photo> SaveInstance(Photo ev);
    }
}
