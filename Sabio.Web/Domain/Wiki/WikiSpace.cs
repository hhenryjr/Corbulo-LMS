using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sabio.Web.Domain.Wiki
{
    public class WikiSpace
    {
        // use for wikispace set 

        public int Id { get; set; }

        public string Title { get; set; }

        public int ParentId { get; set; }
  
    }
}
