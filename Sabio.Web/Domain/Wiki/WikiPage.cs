using System;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace Sabio.Web.Domain.Wiki
{
    public class WikiPage
    {

        public WikiPage()
        {
            Nodes  = new List<WikiPage>();
        }

        public int Id { get; set;}
       
        public string Title { get; set; }
      
        public int Categories { get; set; }
      
        public string URL { get; set; }
       
        public DateTime PublishDate { get; set; }
       
        public int Language { get; set; }

        public DateTime DataAdded { get; set; }

        public DateTime DateModified { get; set; }

        public int ParentId { get; set; }

        public string LastModifiedByUserId { get; set; }

        public int PageType { get; set; }

        public List<WikiSpace> WikiSpaces { get; set; }

        public List<WikiPage> Nodes { get; set; }

        public List<WikiPageContent> Contents { get; set; }

        public List<WikiTags> WikiTags { get; set; }

    }
}