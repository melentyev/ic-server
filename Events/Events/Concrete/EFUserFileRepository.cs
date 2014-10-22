using System.Linq;
using Events.Abstract;
using Events.Models;
using System.Threading.Tasks;

namespace Events.Concrete
{
    public class EFUserFileRepository : Events.Concrete.EFRepository<UserFile>, IUserFileRepository
    {
        public EFUserFileRepository() : base("UserFileId") { }
        public virtual Task<UserFile> FindAsync(params object[] k)
        {
            return context.Set<UserFile>().FindAsync(k);
        }
    }
}