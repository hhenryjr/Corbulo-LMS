using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sabio.Web.Domain.Wiki;
using Sabio.Web.Models.Requests.Wikis;
using Sabio.Web.Models.Requests;
using System.Data.SqlClient;
using System.Data;

namespace Sabio.Web.Services.Interfaces
{
    public interface IWikiService
    {
        int Add(WikiAddRequest model, string userId);

        void Update(WikiUpdateRequest model, string userId);

        void UpdateSpaces(int id, int spaceId);

        void DeleteSpace(int id, int spaceId);

        void updateTags(int id, int tagId);

        void DeleteTag(int id);

        WikiPage GetWiki(int id);

        List<WikiPage> GetWikisByType(int typeId);

        List<WikiPage> GetWikis();

        void Delete(int id);

        void UpdateParent(int pageId, int? parentId);

        List<WikiPage> GetPagesByModule(int moduleId);

        List<WikiPage> GetAllPagesBySpaces();

        List<WikiPage> Getpages();

        List<WikiPage> GetSpaceById(int id);

        List<WikiPage> GetWikePages(Action<SqlParameterCollection> paramMapper, string procName);

        void LinkToTop(Dictionary<int, WikiPage> dic, List<WikiPage> top);

        void LinkPagesToSpaces(List<WikiPageSpace> wikiPageSpaces, List<WikiPage> pages, Dictionary<int, WikiSpace> wikiSpaces);

        void ProcessWikiSpaceData(IDataReader reader, Dictionary<int, WikiSpace> wikiSpaces);

        WikiSpace MapWikiSpace(IDataReader reader);

        WikiPageSpace MapWikPageSpaces(IDataReader reader);

        void ProcessWikiPageData(IDataReader reader, List<WikiPage> pages,
         Dictionary<int, WikiPage> dic, List<WikiPage> top);

        WikiPage MapWikiPage(IDataReader reader);
    }
}