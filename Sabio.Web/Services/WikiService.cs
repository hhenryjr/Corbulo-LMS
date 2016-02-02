using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Data;
using Sabio.Web.Domain.Wiki;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Requests.Wikis;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Services
{
    public class WikiService : BaseService, IWikiService
    {
        public int Get()
        {
            return 0;
        }

        public int Add(WikiAddRequest model, string userId)
        {
            int Id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.WikiPages_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@UserId", userId);
                    paramCollection.AddWithValue("@Name", model.Title);
                    paramCollection.AddWithValue("@URL", model.URL);
                    paramCollection.AddWithValue("@PublishDate", null);
                    paramCollection.AddWithValue("@Language", null);// model.Language);
                    paramCollection.AddWithValue("@LastModifiedByUserId", userId);

                    SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                    p.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(p);

                }, returnParameters: delegate (SqlParameterCollection param)
                {
                    int.TryParse(param["@Id"].Value.ToString(), out Id);
                }
                );

            //foreach (var wikiSpaceId in model.WikiSpaceIds)
            //    DataProvider.ExecuteNonQuery(GetConnection, "dbo.WikiPageSpaces_Insert"
            //        , inputParamMapper: delegate (SqlParameterCollection param)
            //        {
            //            param.AddWithValue("@WikiPageId", Id);
            //            param.AddWithValue("@WikiSpaceId", wikiSpaceId);
            //        });
            return Id;
        }

        public void Update(WikiUpdateRequest model, string userId)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.WikiPages_Update",
                inputParamMapper: delegate (SqlParameterCollection updateWikis)
                {
                    updateWikis.AddWithValue("@LastModifiedByUserId", userId);
                    updateWikis.AddWithValue("@Id", model.Id);
                    updateWikis.AddWithValue("@Name", model.Title);
                    updateWikis.AddWithValue("@URL", model.URL);
                    updateWikis.AddWithValue("@PublishDate", null); // model.PublishDate);
                    updateWikis.AddWithValue("@Language", null);//   model.Language);
                    updateWikis.AddWithValue("@ParentId", model.ParentId);
                });

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.WikiPageSpaces_Delete"
                , inputParamMapper: delegate (SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@WikiPageId", model.Id);

                });

            //if (model.WikiSpaceIds != null)
            //{
            //    foreach (var wikiSpaceId in model.WikiSpaceIds)
            //        DataProvider.ExecuteNonQuery(GetConnection, "dbo.WikiPageSpaces_Insert"
            //            , inputParamMapper: delegate (SqlParameterCollection param)
            //            {
            //                param.AddWithValue("@WikiPageId", model.Id);
            //                param.AddWithValue("@WikiSpaceId", wikiSpaceId);

            //            });
            //}
        }

        //Update & Delete for wikispaces new endpoint  based on CheckBox
       public void UpdateSpaces(int id, int spaceId)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.WikiPageSpaces_Insert"
                     , inputParamMapper: delegate (SqlParameterCollection param)
                     {
                         param.AddWithValue("@WikiPageId", id);
                         param.AddWithValue("@WikiSpaceId", spaceId);
                     });

        }

        public void DeleteSpace(int id, int spaceId)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.WikiSpaces_DeleteByWikiPageId"
               , inputParamMapper: delegate (SqlParameterCollection parameterCollection)
               {
                   parameterCollection.AddWithValue("@WikiPageId", id);
                   parameterCollection.AddWithValue("@WikiSpaceId", spaceId);
               });
        }

        //Update & Delet For WikiTags new Endpoint based on tags select
        public void updateTags(int id, int tagId)
        {
           
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.WikiTags_Insert",
                    inputParamMapper: delegate (SqlParameterCollection param)
                    {
                        param.AddWithValue("@WikiId", id);
                        param.AddWithValue("@TagId", tagId);
                    });
        }

        public void DeleteTag(int id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.WikiTags_DeleteByWikiId"
               , inputParamMapper: delegate (SqlParameterCollection parameterCollection)
               {
                   parameterCollection.AddWithValue("@WikiId", id);
               });
        }


        // Wiki by ID
        public WikiPage GetWiki(int id)
        {
            WikiPage item = null;
            List<WikiPageSpace> wikiPageSpaces = null;

            WikiSpace ws;

            DataProvider.ExecuteCmd(GetConnection, "dbo.WikiPages_SelectByIdv2"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                    //model binding

                }, map: delegate (IDataReader reader, short set)
                {
                    if (set == 0)
                    {
                        item = new WikiPage();
                        int startingIndex = 0; // startingOrdinal

                        item.Id = reader.GetSafeInt32(startingIndex++);
                        item.Title = reader.GetSafeString(startingIndex++);
                        item.URL = reader.GetSafeString(startingIndex++);
                        item.PublishDate = reader.GetSafeDateTime(startingIndex++);
                        item.Language = reader.GetSafeInt32(startingIndex++);
                        item.LastModifiedByUserId = reader.GetSafeString(startingIndex++);
                        item.ParentId = reader.GetSafeInt32(startingIndex++);

                    }
                    else if (set == 1)
                    {
                        WikiPageSpace wps = new WikiPageSpace();
                        int startingIndex = 0;


                        wps.WikiPageId = reader.GetSafeInt32(startingIndex++);
                        wps.WikiSpaceId = reader.GetSafeInt32(startingIndex++);

                        if (wikiPageSpaces == null)
                        {
                            wikiPageSpaces = new List<WikiPageSpace>();
                        }
                        wikiPageSpaces.Add(wps);
                    }
                    else if (set == 2)
                    {
                        ws = new WikiSpace();
                        int startingIndex = 0;

                        ws.Id = reader.GetSafeInt32(startingIndex++);
                        ws.Title = reader.GetSafeString(startingIndex++);
                        ws.ParentId = reader.GetSafeInt32(startingIndex++);

                        if (item != null)
                        {
                            if (wikiPageSpaces != null)
                                foreach (WikiPageSpace wikiPageSpace in wikiPageSpaces)
                                {
                                    if (item.Id == wikiPageSpace.WikiPageId && ws.Id == wikiPageSpace.WikiSpaceId)
                                    {
                                        if (item.WikiSpaces == null)
                                        {
                                            item.WikiSpaces = new List<WikiSpace>();
                                        }
                                        item.WikiSpaces.Add(ws);
                                    }

                                }
                        }

                    }
                    else if (set == 3)
                    {
                        WikiTags wt = new WikiTags();
                        int startingIndex = 0;


                        wt.WikiId = reader.GetSafeInt32(startingIndex++);
                        wt.TagId = reader.GetSafeInt32(startingIndex++);
                        wt.TagName = reader.GetSafeString(startingIndex++);

                        if (item.WikiTags == null)
                        {
                            item.WikiTags = new List<WikiTags>();
                        }
                        item.WikiTags.Add(wt);

                    }
                }

                );

            return item;
        }

        public List<WikiPage> GetWikisByType(int typeId)
        {
            List<WikiPage> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.WikiPages_SelectByType"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@PageTypeId", typeId);

                }
                , map: delegate (IDataReader reader, short set)
                {
                    WikiPage wk = MapWikiPage(reader);

                    if (list == null)
                    {
                        list = new List<WikiPage>();
                    }

                    list.Add(wk);
                }
                );
            return list;
        }

        public List<WikiPage> GetWikis()
        {
            List<WikiPage> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.WikiPages_SelectAll"
                , inputParamMapper: null
                , map: delegate (IDataReader reader, short set)
                {
                    WikiPage wk = new WikiPage();
                    int startingIndex = 0;

                    wk.Id = reader.GetSafeInt32(startingIndex++);
                    wk.Title = reader.GetSafeString(startingIndex++);
                    wk.URL = reader.GetSafeString(startingIndex++);
                    wk.PublishDate = reader.GetSafeDateTime(startingIndex++);
                    wk.Language = reader.GetSafeInt32(startingIndex++);

                    if (list == null)
                    {
                        list = new List<WikiPage>();
                    }

                    list.Add(wk);
                }
                );
            return list;
        }

        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.WikiPages_DeleteById",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);

                });
        }

        // for Wiki Tree
        public void UpdateParent(int pageId, int? parentId)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.WikiPages_UpdateWithDragAndDrop",
                inputParamMapper: delegate (SqlParameterCollection updateParent)
                {
                    updateParent.AddWithValue("@Id", pageId);
                    updateParent.AddWithValue("@ParentId", parentId);

                });
        }

        public List<WikiPage> GetPagesByModule(int moduleId)
        {
            return GetWikePages(delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@ModuleId", moduleId);
            }, "dbo.WikiPages_SelectAllByModuleId");
        }

        public List<WikiPage> GetAllPagesBySpaces()
        {
            return GetWikePages(delegate (SqlParameterCollection paramCollection)
            {

            }, "dbo.WikiPages_SelectAllBySpaces");
        }

        public List<WikiPage> Getpages()
        {
            return GetWikePages(delegate (SqlParameterCollection paramCollection)
            {
                //change this
            }, "dbo.WikiPages_SelectAllV2");
        }

        public List<WikiPage> GetSpaceById(int id)
        {
            return GetWikePages(delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", id);

            }, "dbo.WikiPages_SelectAllBYSpaceId");
        }

        //DRY Principle 

        public List<WikiPage> GetWikePages(Action<SqlParameterCollection> paramMapper, string procName)
        {
            List<WikiPage> pages = null;
            List<WikiPage> top = null;
            Dictionary<int, WikiPage> dic = null;
            List<WikiPageSpace> wikiPageSpaces = null;
            Dictionary<int, WikiSpace> wikiSpaces = null;


            DataProvider.ExecuteCmd(GetConnection, procName
                , inputParamMapper: paramMapper,
                map: delegate (IDataReader reader, short set)
                {
                    if (set == 0)
                    {
                        if (pages == null)
                        {
                            pages = new List<WikiPage>();
                        }

                        if (dic == null)
                        {
                            dic = new Dictionary<int, WikiPage>();
                        }

                        if (top == null)
                        {
                            top = new List<WikiPage>();
                        }

                        ProcessWikiPageData(reader, pages, dic, top);
                    }
                    else if (set == 1)
                    {
                        WikiPageSpace wps = MapWikPageSpaces(reader);

                        if (wikiPageSpaces == null)
                        {
                            wikiPageSpaces = new List<WikiPageSpace>();
                        }

                        wikiPageSpaces.Add(wps);
                    }
                    else if (set == 2)
                    {
                        if (wikiSpaces == null)
                        {
                            wikiSpaces = new Dictionary<int, WikiSpace>();

                        }

                        ProcessWikiSpaceData(reader, wikiSpaces);
                    }
                    else if (set == 3)
                    {
                        WikiTags wt = new WikiTags();
                        int startingIndex = 0;


                        wt.WikiId = reader.GetSafeInt32(startingIndex++);
                        wt.TagId = reader.GetSafeInt32(startingIndex++);
                        wt.TagName = reader.GetSafeString(startingIndex++);

                        WikiPage parent = dic[wt.WikiId];
                        if (parent.WikiTags == null)
                        {
                            parent.WikiTags = new List<WikiTags>();
                        }
                        parent.WikiTags.Add(wt);

                    }
                }
                );

            LinkPagesToSpaces(wikiPageSpaces, pages, wikiSpaces);

            LinkToTop(dic, top);

            return top;
        }

        public void LinkToTop(Dictionary<int, WikiPage> dic, List<WikiPage> top)
        {
            if (dic != null)
            {
            foreach (var dictEntry in dic)
            {
                WikiPage page = dictEntry.Value;

                if (page.ParentId != 0)
                {
                    WikiPage parent = null;

                    if (dic.ContainsKey(page.ParentId))
                    {
                        parent = dic[page.ParentId];
                    }

                    if (parent != null)
                    {
                        parent.Nodes.Add(page);
                    }
                    else
                    {
                        if (top == null)
                        {
                            top = new List<WikiPage>();
                        }

                        page.Title = HttpUtility.HtmlDecode(" -- ") + page.Title;
                        top.Add(page);
                    }
                }
            }
            }
        }

        public void LinkPagesToSpaces(List<WikiPageSpace> wikiPageSpaces, List<WikiPage> pages, Dictionary<int, WikiSpace> wikiSpaces)
        {
            if (wikiPageSpaces != null)
            {
            foreach (WikiPageSpace wikiPageSpace in wikiPageSpaces)
            {
                foreach (WikiPage wikiPage in pages)
                {
                    if (wikiPage.Id == wikiPageSpace.WikiPageId)
                    {
                        if (wikiPage.WikiSpaces == null)
                        {
                            wikiPage.WikiSpaces = new List<WikiSpace>();
                        }

                        wikiPage.WikiSpaces.Add(wikiSpaces[wikiPageSpace.WikiSpaceId]);
                    }
                }
            }
        }
        }

        public void ProcessWikiSpaceData(IDataReader reader, Dictionary<int, WikiSpace> wikiSpaces)
        {
            WikiSpace ws = MapWikiSpace(reader);
            wikiSpaces[ws.Id] = ws;
        }

        public WikiSpace MapWikiSpace(IDataReader reader)
        {
            WikiSpace ws = new WikiSpace();
            int startingIndex = 0;

            ws.Id = reader.GetSafeInt32(startingIndex++);
            ws.Title = reader.GetSafeString(startingIndex++);
            ws.ParentId = reader.GetSafeInt32(startingIndex++);
            return ws;
        }

        public WikiPageSpace MapWikPageSpaces(IDataReader reader)
        {
            WikiPageSpace wps = new WikiPageSpace();
            int startingIndex = 0;

            wps.WikiPageId = reader.GetSafeInt32(startingIndex++);
            wps.WikiSpaceId = reader.GetSafeInt32(startingIndex++);
            return wps;
        }

        public void ProcessWikiPageData(IDataReader reader, List<WikiPage> pages,
         Dictionary<int, WikiPage> dic, List<WikiPage> top)
        {
            WikiPage wiki = MapWikiPage(reader);


            pages.Add(wiki);

            if (!dic.ContainsKey(wiki.Id))
                dic.Add(wiki.Id, wiki);

            if (wiki.ParentId == 0)
            {
                top.Add(wiki);
            }

        }

        public WikiPage MapWikiPage(IDataReader reader)
        {
            WikiPage wiki = new WikiPage();
            int startingIndex = 0;

            wiki.Id = reader.GetSafeInt32(startingIndex++);
            wiki.Title = reader.GetSafeString(startingIndex++);
            wiki.URL = reader.GetSafeString(startingIndex++);
            wiki.PublishDate = reader.GetSafeDateTime(startingIndex++);
            wiki.Language = reader.GetSafeInt32(startingIndex++);
            wiki.ParentId = reader.GetSafeInt32(startingIndex++);
            return wiki;
        }

    }
}













