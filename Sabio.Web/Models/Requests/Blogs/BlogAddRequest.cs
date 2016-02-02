using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Sabio.Web.Models.Requests.Blogs
{
    public class BlogAddRequest
    {
        [Required]
        [MaxLength(20)]
        public string Title { get; set; }

       
        public DateTime? PublishedDate { get; set; }

        [Required]
        [MinLength(2)]
        public string BlogPost { get; set; }

        public string Tags { get; set; }

        public bool IsFeatured { get; set; }
    }
}