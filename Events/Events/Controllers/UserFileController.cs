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
namespace Events.Controllers
{
    public class UserFileController : ApplicationApiController
    {
        private IUserFileRepository userFileRepository;
        public UserFileController(IUserFileRepository UFRepo)
        {
            userFileRepository = UFRepo;
        }
        [Route("GetUploadUrl/{purpose}")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> GetUploadUrl(string purpose)
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
        [ResponseType(typeof(FileAnswer))]
        public async Task<IHttpActionResult> Upload(IEnumerable<System.Web.HttpPostedFileBase> uploads)
        {
            string forHash= "";
            var answer = new FileAnswer();
            foreach (var file in uploads)
            {
                if (file != null)
                {
                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileName(file.FileName) + CurrentUser.UserId.ToString() + DateTime.Now.ToString();
                    // сохраняем файл в папку Files в проекте
                    file.SaveAs(HttpContext.Current.Server.MapPath("~/Files/" + fileName));
                    var userFile = new UserFile
                    {
                        UserId = CurrentUser.UserId,
                        FilePath = "~/Files/" + fileName,
                        FileSize = file.ContentLength,
                        State = UserFileState.JustUploaded,
                        DateCreate = DateTime.Now,
                        Hash = file.GetHashCode(),
                    };
                    var addedFile = await userFileRepository.SaveInstance(userFile);
                    answer.FileId.Add(addedFile.UserFileId);
                    forHash = forHash + addedFile.UserFileId.ToString() + addedFile.FileSize.ToString();
                }
            }
            answer.Hash = forHash.GetHashCode();
            return Ok(answer);
        }

        [Route("File.Save")]
        public async Task<IHttpActionResult> Save(FileAnswer model)
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
                if (fileId != null)
                {
                    var dbEntry = await userFileRepository.Objects.Where(e => e.UserFileId == fileId).FirstOrDefaultAsync();
                    if (dbEntry == null)
                    {
                        return BadRequest();
                    }
                    UFList.Add(dbEntry);
                    forHash = forHash + dbEntry.UserFileId.ToString() + dbEntry.FileSize.ToString();
                }
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
                }
                return Ok(Path);
            }
            return BadRequest();
        }
    }
}
