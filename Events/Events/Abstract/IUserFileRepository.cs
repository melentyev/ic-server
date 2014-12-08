using System;
using System.Linq;
using Events.Models;
using System.Threading.Tasks;

namespace Events.Abstract
{
    public interface IUserFileRepository : IDisposable
    {
        IQueryable<UserFile> Objects { get; }
        Task<UserFile> FindAsync(params object[] k);
        Task<UserFile> SaveInstance(UserFile userFile);
    }
}