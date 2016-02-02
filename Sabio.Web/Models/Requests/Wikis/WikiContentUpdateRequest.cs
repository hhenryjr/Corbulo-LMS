using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.Wikis
{
    public class WikiContentUpdateRequest
    {
        [Required]
        public string PageContent { get; set; }

        public Dictionary<string, string> ContentOptions { get; set; }

        public List<WikiContentListRequest> ContentData { get; set; }

        public string Title { get; set; }
    }
}