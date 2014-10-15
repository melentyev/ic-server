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
    [Authorize]
    public class EndpointsController : ApplicationApiController
    {
        public IGcmRegIdsRepository regIdsRepo;
        public EndpointsController(IGcmRegIdsRepository repo)
        {
            regIdsRepo = repo;
        }

        [Route("GcmRegister/{regId}")]
        [CheckModelForNull]
        public async Task<IHttpActionResult> GcmRegister(string regId)
        {
            var model = await regIdsRepo.FindAsync(regId);
            return Ok();
        }
    }
}
