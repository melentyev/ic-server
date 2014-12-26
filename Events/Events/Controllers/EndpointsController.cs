using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
using System.IO;

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
        public EndpointsController(IGcmRegIdsRepository repo, IUserFileRepository puserFileRepository)
        {
            regIdsRepo = repo;
            userFileRepository = puserFileRepository;
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
        [Route("GetUploadUrl/{purpose}")]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetUploadUrl(string purpose)
        {
            switch (purpose)
            {
                case "AddEvent":
                    return Ok("/api/Endpoints/Upload");
                default:
                    return BadRequest("incorrect input");
            }

        }

        [Route("Upload")]
        [ResponseType(typeof(SaveFileBindingModel))]
        [Authorize]
        public async Task<IHttpActionResult> PostUpload()
        {
            string forHash = "";
            var answer = new SaveFileBindingModel() { FileId = new List<int>() { } };
            HttpRequestMessage request = this.Request;
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/uploads/");
            var provider = new MultipartFormDataStreamProvider(root);

            await request.Content.ReadAsMultipartAsync(provider);
            MultipartFileData file = provider.FileData.First();
            {
                var originalName = file.Headers.ContentDisposition.FileName;
                if (file == null)
                {
                    return BadRequest("Not file in input");
                }
                try { 
                var userFile = new UserFile
                {
                    UserId = CurrentUser.UserId,
                    FilePath = file.LocalFileName,
                    FileSize = (int)new System.IO.FileInfo(file.LocalFileName).Length,
                    State = UserFileState.JustUploaded,
                    DateCreate = DateTime.Now,
                    ServerId = 1
                };
               
                var addedFile = await userFileRepository.SaveInstance(userFile);
                answer.FileId.Add(addedFile.UserFileId);
                forHash = forHash + addedFile.UserFileId.ToString() + addedFile.FileSize.ToString();
                }
                catch (Exception e)
                {
                    var a = 5;
                }
            }
            answer.Hash = forHash.GetHashCode();
            return Ok(answer);
        }
        [Route("GetFile/{fileId}")]
        public async Task<HttpResponseMessage> GetFile(string fileId)
        {
            var id = Int32.Parse(fileId);
            string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/uploads/");

            var file = await userFileRepository.Objects.Where(f => f.UserFileId == id).FirstOrDefaultAsync();
            var path = file.FilePath;
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            return result;
        }

        [Route("SaveUploadedFile/{purpose}")]
        public async Task<IHttpActionResult> PostSave(string purpose, SaveFileBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fileEntries = await userFileRepository.Objects.Where(e => model.FileId.Contains(e.UserFileId)).ToListAsync();
            if (fileEntries.Count != model.FileId.Count)
            {
                return BadRequest();
            }
            var forHash = fileEntries.Aggregate("", (h, f) => h + f.UserFileId.ToString() + f.FileSize.ToString());
            if (forHash.GetHashCode() != model.Hash)
            {
                return BadRequest();
            }
            await Task.WhenAll(fileEntries.Select(f => {
                f.State = UserFileState.Saved;
                return userFileRepository.SaveInstance(f);
            }));
            var result = fileEntries.Select(f => new SavedFileViewModel { Id = f.UserFileId, FileSize = f.FileSize, Url = f.FilePath});
            return Ok(result);
        }
    }
}
