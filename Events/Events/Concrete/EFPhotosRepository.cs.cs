using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

using Events.Abstract;
using Events.Models;

namespace Events.Concrete
{
    public class EFPhotosRepository : Events.Concrete.EFRepository<Photo>, IPhotosRepository
    {
        public EFPhotosRepository() : base("PhotoId") { }

        public virtual Task<Photo> FindAsync(params object[] k)
        {
            return context.Set<Photo>().FindAsync(k);
        }
    }
}