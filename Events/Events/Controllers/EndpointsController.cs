using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;

namespace Events.Models
{
    [Authorize]
    public class EndpointsController : ApiController
    {
        public EndpointsController()
        {

        }

        [Route("GcmRegister/{regId}")]
        public async Task<IHttpActionResult> GcmRegister(string regId)
        {

            return Ok();
        }
    }
}
