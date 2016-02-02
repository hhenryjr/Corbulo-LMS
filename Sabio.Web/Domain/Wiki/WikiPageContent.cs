using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain.Wiki
{
    public class WikiPageContent
    {
        public int Id {get;set;}
        public string Title { get; set; }
        public int SortOrder {get;set;}
        public string PageContent { get; set; }
        public int ContentTypeId {get;set;}
        public int WikiPageId {get;set;}
        public Dictionary<string, string> ContentOptions {get;set;}
        public List<WikiListContent> ContentData { get; set; }   
        public File ContentFile { get; set; } 
    }
}