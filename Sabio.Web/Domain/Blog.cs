using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Blog
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime DatePublished { get; set; }

        public string BlogPost { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime DateModified { get; set; }
        
        public List<BlogTag> Tags { get; set; }
    }
}