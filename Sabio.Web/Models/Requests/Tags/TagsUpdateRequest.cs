using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.Tags
{
    public class TagsUpdateRequest : TagsAddRequest
    {
        [Required]
        public int Id { get; set; }
    }
}