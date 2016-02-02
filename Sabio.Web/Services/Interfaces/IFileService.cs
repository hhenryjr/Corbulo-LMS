using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services.Interfaces
{
    public interface IFileService
    {
        string UploadToCloud(HttpPostedFile file);

        int Add(string fileName, int fileTypeId);

        string GetFullUrl(string path);

        File GetFileByWikiContentId(int wikiContentId);
    }
}
