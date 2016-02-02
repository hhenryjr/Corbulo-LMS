using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class WikiPagesFilterRequest
    {
        public WikiPagesFilterRequest() {

            
            PageSize = 10;
            PageIndex = 0;

        }
        
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }


}