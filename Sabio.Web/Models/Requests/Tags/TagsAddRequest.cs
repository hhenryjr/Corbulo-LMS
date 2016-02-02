using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.Tags
{
    public class TagsAddRequest
    {
        [Required]
        [MaxLength(100)]
        public string TagName { get; set; }

        //[Required]
        public bool Approved { get; set; }


    }
}