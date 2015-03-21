using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

using Events.Infrastructure;
using Events.Models;
using Events.Abstract;
using Events.Filters;

namespace Events.Controllers
{
    [RoutePrefix("api/Users")]
    public class UsersController : ApplicationApiController
    {
        private IDataRepository dataRepo;

        public UsersController(IDataRepository pDataRepo)
        {
            dataRepo = pDataRepo;
        }
        
        [ResponseType(typeof(UserProfileViewModel))]
        [Route("UserProfile/{userId}")]
        public async Task<IHttpActionResult>  GetUserProfile(int userId)
        {
            var user = await dataRepo.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            var photo = await dataRepo.Photos.Where(p => p.PhotoId == user.PhotoId).FirstOrDefaultAsync();
            var file = photo == null ? null : await dataRepo.UserFiles.Where(f => f.UserFileId == photo.UserFileId).FirstOrDefaultAsync();

            var profile = new UserProfileViewModel(user, photo, file);
            return Ok(profile);
        }
    }
}
