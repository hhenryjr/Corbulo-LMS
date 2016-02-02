using Sabio.Web.Models.Requests;
using Sabio.Web.Services;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using Amazon.S3.Model;
using System.Web.UI.WebControls;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests.UsersData;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/files")]
    public class FileApiController : ApiController
    {
        private IFileService _fileService;
        private IWikiContentService _wikiContentService;
        //private IUserService _userService;

        public FileApiController(IFileService fileService, IWikiContentService wikiContentService/*, IUserService UserService*/)
        {
            _fileService = fileService;
            _wikiContentService = wikiContentService;
            //_userService = UserService;
        }

        //upload file and save record data coming from AWS        
        [Route("{fileTypeId:int}"), HttpPost]
        public HttpResponseMessage UploadFile(int fileTypeId)
        {
          
            List<FileBase> data = null;
            for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
            {
                string userId = UserService.GetCurrentUserId();

                HttpPostedFile file = HttpContext.Current.Request.Files[i];
                if (file.ContentLength > 0)
                {
                    if (data == null)
                    {
                        data = new List<FileBase>();
                    }
                    var path = _fileService.UploadToCloud(file);
                    var fileId = _fileService.Add(path, fileTypeId);
          
                    data.Add(new FileBase { FileId = fileId, Path = "https://sabio-training.s3-us-west-2.amazonaws.com/C11/" + path, FileTypeId = fileTypeId });
    
                }

            }

            ItemsResponse<FileBase> response = new ItemsResponse<FileBase>();

            response.Items = data;

            return Request.CreateResponse(response);
        }

        [Route("wikiContent/{wikiPageContentId:int}"), HttpPost]
        public HttpResponseMessage UploadContentFile(int wikiPageContentId)
        {

            List<FileBase> data = null;
            for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
            {
                string userId = UserService.GetCurrentUserId();

                HttpPostedFile file = HttpContext.Current.Request.Files[i];
                if (file.ContentLength > 0)
                {
                    if (data == null)
                    {
                        data = new List<FileBase>();
                    }

                    var path = _fileService.UploadToCloud(file);
                    var fileId = _fileService.Add(path, 2);
                    var wikiContentId = _wikiContentService.AddWikiPageContentFile(fileId, wikiPageContentId);

                    data.Add(new FileBase { FileId = fileId, Path = "//sabio-training.s3-us-west-2.amazonaws.com/C11/" + path, FileTypeId = wikiContentId });

                }

            }

            ItemsResponse<FileBase> response = new ItemsResponse<FileBase>();

            response.Items = data;

            return Request.CreateResponse(response);
        }

    }
}
