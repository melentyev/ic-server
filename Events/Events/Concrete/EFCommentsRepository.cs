using System.Linq;
using Events.Abstract;
using Events.Models;
using System.Threading.Tasks;

namespace Events.Concrete
{
    public class EFCommentsRepository : Events.Concrete.EFRepository<Comment>, ICommentsRepository
    {
        public EFCommentsRepository() : base("CommentId") { }
        public virtual Task<Comment> FindAsync(params object[] k)
        {
            return context.Set<Comment>().FindAsync(k);
        }
    }
}