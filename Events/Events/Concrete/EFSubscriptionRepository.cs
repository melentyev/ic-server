using System.Linq;
using Events.Abstract;
using Events.Models;
using System.Threading.Tasks;

namespace Events.Concrete
{
    public class EFSubscriptionRepository : Events.Concrete.EFRepository<Subscription>, ISubscribeRepository
    {
        public EFSubscriptionRepository() : base("CommentId") { }
    }
}