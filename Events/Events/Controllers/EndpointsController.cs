using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Events.Infrastructure;
using Events.Models;
using Events.Abstract;
using Events.Filters;


namespace Events.Models
{
    [RoutePrefix("api/Endpoints")]
    //[Authorize]
    public class EndpointsController : ApplicationApiController
    {
        private IGcmRegIdsRepository regIdsRepo;
        private IUserFileRepository userFileRepository;
        public EndpointsController(IGcmRegIdsRepository repo, IUserFileRepository UFRepo)
        {
            regIdsRepo = repo;
            userFileRepository = UFRepo;
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

        [Route("GetUploadUrl/{purpose}")]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetUploadUrl(string purpose)
        {
            switch (purpose)
            {
                case "AddEvent":
                    return Ok("/api/Endpoints/Upload");
                    break;
                default:
                    return BadRequest("incorrect input");
                    break;
            }

        }

        [Route("Upload/{uploads}")]
        [ResponseType(typeof(SaveFileBindingModel))]
        public async Task<IHttpActionResult> Upload(IEnumerable<System.Web.HttpPostedFileBase> uploads)
        {
            string forHash = "";
            var answer = new SaveFileBindingModel();
            foreach (var file in uploads)
            {
                if (file != null)
                {
                    var userFile = new UserFile
                    {
                        UserId = CurrentUser.UserId,
                        FilePath = "",
                        FileSize = file.ContentLength,
                        State = UserFileState.JustUploaded,
                        DateCreate = DateTime.Now,
                    };
                    var addedFile = await userFileRepository.SaveInstance(userFile);
                    // имя - номер записи в базе
                    string fileName = addedFile.UserFileId.ToString();
                    // сохраняем файл в папку Files в проекте
                    file.SaveAs(HttpContext.Current.Server.MapPath("~/Files/" + fileName));
                    addedFile.FilePath = ("~/Files/" + fileName);
                    addedFile = await userFileRepository.SaveInstance(userFile);
                    answer.FileId.Add(addedFile.UserFileId);
                    forHash = forHash + addedFile.UserFileId.ToString() + addedFile.FileSize.ToString();
                }
                else
                {
                    BadRequest("Not file in input");
                }
            }
            answer.Hash = forHash.GetHashCode();
            return Ok(answer);
        }

        [Route("api/Endpoints/SaveUploadedFile/")]
        public async Task<IHttpActionResult> Save(SaveFileBindingModel model)
        {
            List<UserFile> UFList = null;
            string forHash = "";
            List<string> Path = null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var fileId in model.FileId)
            {
                var dbEntry = await userFileRepository.Objects.Where(e => e.UserFileId == fileId).FirstOrDefaultAsync();
                if (dbEntry == null)
                {
                    return BadRequest();
                }
                UFList.Add(dbEntry);
                forHash = forHash + dbEntry.UserFileId.ToString() + dbEntry.FileSize.ToString();
            }
            if (forHash.GetHashCode() == model.Hash)
            {
                foreach (var userFile in UFList)
                {
                    if (userFile != null)
                    {
                        userFile.State = UserFileState.Saved;
                        Path.Add(userFile.FilePath);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                return Ok(Path);
            }
            return BadRequest();
        }
    }
}