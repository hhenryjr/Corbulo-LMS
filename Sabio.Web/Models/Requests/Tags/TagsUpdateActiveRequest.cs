using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.Tags
{
    public class TagsUpdateActiveRequest
    {
        [Required]
        public int Id { get; set; }

        public bool Approved { get; set; }
    }
}