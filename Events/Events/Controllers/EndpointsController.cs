using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
            if (String.IsNullOrEmpty(regId))
            {
                return BadRequest();
            }
            var reg = new GcmRegistrationId { RegId = regId, UserId = CurrentUser.UserId };
            await regIdsRepo.SaveInstance(reg);
            //var model = await regIdsRepo.FindAsync(regId);
            return Ok();
        }
    }
}


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace ForM.info
//{
//    public partial class informat : System.Web.UI.Page
//    {
//        protected void Page_Load(object sender, EventArgs e)
//        {

//        }

//        protected void Button1_Click(object sender, EventArgs e)
//        {

//            if (FileUpload1.HasFile)
//            {
//                string fileName = FileUpload1.FileName;

//                FileUpload1.SaveAs(Server.MapPath(@"~\img\") + fileName);
//                TextBox1.Text = "Сохранено";

//            }
//        }
//    }
//}