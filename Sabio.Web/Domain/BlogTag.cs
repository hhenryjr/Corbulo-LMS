using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class BlogTag
    {
        public int BlogId { get; set; }

        public int TagId { get; set; }

        public virtual string TagName { get; set; }



    }
}