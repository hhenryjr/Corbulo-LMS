using Sabio.Web.Domain.Wiki;
using Sabio.Web.Models.Requests.Wikis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Web.Services.Interfaces
{
    public interface IWikiContentService
    {
        WikiPage GetPageBySlug(string wikiSlug);

        List<WikiPageContent> GetContentByPageId(int pageId);

        int InsertContent(WikiContentCreateRequest request);

        void UpdateContent(int ContentId, WikiContentUpdateRequest request);

        void DeleteContent(int ContentId);

        int AddWikiPageContentFile(int fileId, int wikiPageContentId);

        void UpdateContentOrders(WikiContentOrderRequest request);

        void UpdateContentOrder(WikiContentOrder request);
    }
}
