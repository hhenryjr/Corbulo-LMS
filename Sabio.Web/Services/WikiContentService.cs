using Sabio.Web.Domain.Wiki;
using Sabio.Web.Services.Interfaces;
using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Data;
using Sabio.Web.Models.Requests.Wikis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Sabio.Web.Services
{
    public class WikiContentService : BaseService, IWikiContentService
    {
        private IFileService _fileService;
        private IWikiService _wikiService;

        public WikiContentService(IFileService fileService, IWikiService wikiService)
        {
            _fileService = fileService;
            _wikiService = wikiService;
        }

        public WikiPage GetPageBySlug(string wikiSlug)
        {
            int wikiPageId = 0;

            DataProvider.ExecuteCmd(GetConnection, "dbo.WikiPages_SelectIdBySlug",
                inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Slug", wikiSlug);

                }, map: delegate(IDataReader reader, short set)
                {
                    wikiPageId = reader.GetSafeInt32(0);
                }
                );

            if(wikiPageId > 0)
            {
                WikiPage page = _wikiService.GetWiki(wikiPageId);

                page.Contents = GetContentByPageId(page.Id);

                return page;
            }

            return null;
        }

        public void UpdateContentOrders(WikiContentOrderRequest request)
        {
            foreach(WikiContentOrder order in request.Content)
            {
                UpdateContentOrder(order);
            }
        }

        public void UpdateContentOrder(WikiContentOrder request)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.WikiPageContents_UpdateOrder"
                , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@ContentId", request.Id);
                    paramCollection.AddWithValue("@SortOrder", request.Order);                    
                }, returnParameters: delegate(SqlParameterCollection param)
                {
                    //  nope
                });
        }

        public void UpdateContent(int ContentId, WikiContentUpdateRequest request)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.WikiPageContents_Update"
                , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@ContentId", ContentId);                    
                    paramCollection.AddWithValue("@PageContent", request.PageContent);
                    paramCollection.AddWithValue("@ContentOptions", (request.ContentOptions == null) ? null : JsonConvert.SerializeObject(request.ContentOptions, new KeyValuePairConverter()));
                    paramCollection.AddWithValue("@ContentData", (request.ContentData == null) ? null : JsonConvert.SerializeObject(request.ContentData, new KeyValuePairConverter()));                    
                    paramCollection.AddWithValue("@Title", request.Title);             

                }, returnParameters: delegate(SqlParameterCollection param)
                {
                    //  nope
                });
        }

        public int InsertContent(WikiContentCreateRequest request)
        {
            int id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.WikiPageContents_Insert"
                , inputParamMapper: delegate(SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@ContentTypeId", request.ContentTypeId);
                    paramCollection.AddWithValue("@WikiPageId", request.WikiPageId);

                    paramCollection.AddWithValue("@PageContent", null);
                    paramCollection.AddWithValue("@ContentOptions", null);
                    paramCollection.AddWithValue("@ContentData", null);                    
                    paramCollection.AddWithValue("@Title", request.Title?? "");                   
                    paramCollection.AddWithValue("@SortOrder", request.SortOrder ?? null);

                    SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                    p.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(p);

                }, returnParameters: delegate(SqlParameterCollection param)
                {
                    int.TryParse(param["@Id"].Value.ToString(), out id);
                });

            return id;
        }

        public List<WikiPageContent> GetContentByPageId(int pageId)
        {
            List<WikiPageContent> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.WikiPagesContents_SelectByPageId"
               , inputParamMapper: delegate(SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@PageId", pageId);

               }
               , map: delegate(IDataReader reader, short set)
               {
                   WikiPageContent content = _MapContent(reader);
                   if (list == null)
                   {
                       list = new List<WikiPageContent>();
                   }
                   list.Add(content);
               }
               );
            return list;
        }

        public int AddWikiPageContentFile(int fileId, int wikiPageContentId)
        {
            int id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.WikiPageContentFiles_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@FileId", fileId);
                    paramCollection.AddWithValue("@ContentId", wikiPageContentId);

                    SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                    p.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(p);

                }, returnParameters: delegate (SqlParameterCollection param)
                {
                    int.TryParse(param["@Id"].Value.ToString(), out id);
                });

            return id;
        }

        private WikiPageContent _MapContent(IDataReader reader)
        {
            int startingIndex = 0;

            WikiPageContent content = new WikiPageContent();
            //File file = new FileUploadService();

            content.Id = reader.GetSafeInt32(startingIndex++);
            content.SortOrder = reader.GetSafeInt32(startingIndex++);
            content.PageContent = reader.GetSafeString(startingIndex++);
            content.ContentTypeId = reader.GetSafeInt32(startingIndex++);
            content.WikiPageId = reader.GetSafeInt32(startingIndex++);
            content.Title = reader.GetSafeString(startingIndex++);

            string contentOptions = reader.GetSafeString(startingIndex++);
            string contentData = reader.GetSafeString(startingIndex++);

            if(null != contentOptions)
                content.ContentOptions = JsonConvert.DeserializeObject<Dictionary<string, string>>(contentOptions);

            if (null != contentData)
                content.ContentData = JsonConvert.DeserializeObject<List<WikiListContent>>(contentData);

            if (content.ContentTypeId == (int) WikiPageContentType.Image)
            { 
                content.ContentFile = _fileService.GetFileByWikiContentId(content.Id);                    
            }

            return content;
        }

        public void DeleteContent(int ContentId)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.WikiPageContents_DeleteByContentId",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", ContentId);

                });
        }
    }
}