using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Events.Filters;
using Events.Abstract;
using Events.Infrastructure;

namespace Events.Models
{
    [RoutePrefix("api/Endpoints")]
    //[Authorize]
    public class EndpointsController : ApplicationApiController
    {
        private IGcmRegIdsRepository regIdsRepo;
        public EndpointsController(IGcmRegIdsRepository repo)
        {
            regIdsRepo = repo;
        }

        [Route("GcmRegister/{regId}")]
        [CheckModelForNull]
        public async Task<IHttpActionResult> GcmRegister(string regId)
        {
            if (String.IsNullOrEmpty(regId) ) {
                return BadRequest();
            }
            var reg = new GcmRegistrationId { RegId = regId, UserId = CurrentUser.UserId };
            await regIdsRepo.SaveInstance(reg);
            //var model = await regIdsRepo.FindAsync(regId);
            return Ok();
        }
    }
}
