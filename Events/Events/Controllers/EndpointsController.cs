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
        const int randomDirNameLen = 20;
        private IGcmRegIdsRepository regIdsRepo;
        private IUserFileRepository userFileRepository;
        private char[] fileNameChars = 
            Enumerable.Range('a', 'z' - 'a' + 1)
                .Concat(Enumerable.Range('0', '9' - '0' + 1))
                .Concat(Enumerable.Repeat((int)'-', 1))
                .Select(c => (char)c).ToArray();

        public static string UploadsFolder = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/");
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
                case "UpdateUserPic":
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string forHash = "";
            var answer = new SaveFileBindingModel() { FileId = new List<int>() { } };
            HttpRequestMessage request = this.Request;
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            var rnd = new System.Random((int)DateTime.Now.Ticks + Request.GetHashCode());

            Func<int, string> randomFileName = (n) => 
                new String(
                    Enumerable.Range(1, n)
                    .Select(x => fileNameChars[rnd.Next(fileNameChars.Length)])
                    .ToArray());

            string newDir;
            DirectoryInfo di;
            do
            {
                newDir = randomFileName(randomDirNameLen);
                di = Directory.CreateDirectory(UploadsFolder + newDir);
            } while (di.CreationTime.AddSeconds(10) < DateTime.Now);
            string root = UploadsFolder + newDir + @"\";
            var provider = new MultipartFormDataStreamProvider(root);

            await request.Content.ReadAsMultipartAsync(provider);
            MultipartFileData file = provider.FileData.First();
            if (file == null)
            {
                return BadRequest("Not file in input");
            }
            var originalName = file.Headers.ContentDisposition.FileName;
            var newName = randomFileName(10) + Path.GetExtension(originalName);
            var newLocalName = UploadsFolder + newDir + @"\" + newName;
            File.Move(file.LocalFileName, newLocalName);
                
            var userFile = new UserFile
            {
                UserId = CurrentUser.UserId,
                FilePath = newDir + @"/" + newName,
                FileSize = (int)new System.IO.FileInfo(newLocalName).Length,
                State = UserFileState.JustUploaded,
                DateCreate = DateTime.Now,
                ServerId = 1
            };
               
            var addedFile = await userFileRepository.SaveInstance(userFile);
            answer.FileId.Add(addedFile.UserFileId);
            forHash = forHash + addedFile.UserFileId.ToString() + addedFile.FileSize.ToString();

            answer.Hash = forHash.GetHashCode();
            return Ok(answer);
        }
        [Route("GetFile/{fileId}")]
        public async Task<HttpResponseMessage> GetFile(string fileId)
        {
            var id = Int32.Parse(fileId);

            var file = await userFileRepository.Objects.Where(f => f.UserFileId == id).FirstOrDefaultAsync();
            var path = UploadsFolder + file.FilePath;
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
