using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Data;
using Sabio.Web.Models.Requests;
using System.Web.Mvc;
using Glimpse.AspNet.Tab;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System.Configuration;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Services
{
    public class FileService : BaseService, IFileService
    {
        private static readonly string _awsAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        private static readonly string _awsSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
        private static readonly string _bucketName = ConfigurationManager.AppSettings["Bucketname"];
        private static readonly string _awsBaseUrl = ConfigurationManager.AppSettings["AWSBaseUrl"];
        private static readonly string _awsFolderName = ConfigurationManager.AppSettings["AWSFolderName"];

        //private IUserService _userService;

        //public FileService(IUserService UserService)
        //{
        //    _userService = UserService;
        //}

        public string UploadToCloud(HttpPostedFile file)
        {
            PutObjectResponse response = new PutObjectResponse();
            string fileName;

            try
            {
                string name = file.FileName.Replace("\"", ""); //this line actually does nothing
                fileName = Guid.NewGuid().ToString() + "_" + name;

                IAmazonS3 client;
                using (client = new AmazonS3Client(_awsAccessKey, _awsSecretKey, RegionEndpoint.USWest2))
                {
                    var request = new PutObjectRequest()
                    {
                        BucketName = _bucketName,
                        Key = string.Format("C11/{0}", fileName),
                        InputStream = file.InputStream
                    };
                    response = client.PutObject(request);
                }

            }

            catch
            {
                throw;
            }

            return fileName;
        }
        
        //Add Server PATH/URL to DataBase
        public int Add(string fileName, int fileTypeId)
        {
            var id = 0;
            string userId = UserService.GetCurrentUserId();

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Files_Insert",
                             
                inputParamMapper: delegate(SqlParameterCollection parameterCollection)
                {                 
                    parameterCollection.AddWithValue("@Path", fileName);
                    parameterCollection.AddWithValue("@UserId", userId);
                    parameterCollection.AddWithValue("@FileTypeId", fileTypeId);
                    parameterCollection.AddWithValue("@InUse", true);

                    SqlParameter s = new SqlParameter("@FileId", System.Data.SqlDbType.Int);
                    s.Direction = System.Data.ParameterDirection.Output;
                    parameterCollection.Add(s);
                },

                returnParameters: delegate(SqlParameterCollection para)
                {                 
                    int.TryParse(para["@FileId"].Value.ToString(), out id);
                }
                
                );

            return id;
        }

        public string GetFullUrl(string path)
        {
           return "//" + _bucketName + "." + _awsBaseUrl + "/" + _awsFolderName + "/" + path;
        }

        public File GetFileByWikiContentId(int wikiContentId)
        {
            File item = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.WikiPageContentFiles_SelectByWikiContentId"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@WikiContentId", wikiContentId);

               }
               , map: delegate (IDataReader reader, short set)
               {
                   item = new File();
                   int startingIndex = 0;

                   item.WikiContentId = reader.GetSafeInt32(startingIndex++);
                   item.FileId = reader.GetSafeInt32(startingIndex++);
                   item.Path = reader.GetSafeString(startingIndex++);
                   item.UserId = reader.GetSafeString(startingIndex++);
                   item.FileTypeId = reader.GetSafeInt32(startingIndex++);
                   item.Url = GetFullUrl(item.Path);
               }
               );
            return item;
        }
    }
}
