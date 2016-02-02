using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.Wikis
{
    public class WikiContentCreateRequest
    {
        [Required]
        public int ContentTypeId { get; set; }

        [Required]
        public int WikiPageId { get; set; }

        public string Title { get; set; }

        public int? SortOrder { get; set; }
    }
}