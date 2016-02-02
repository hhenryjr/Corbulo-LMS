using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain.Wiki
{
    public class WikiTags
    {
        public int WikiId { get; set; }

        public int TagId { get; set; }

        public virtual string TagName { get; set; }
    }
}