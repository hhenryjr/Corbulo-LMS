using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.Sections
{
    public class SectionsUpdateRequest : SectionsAddRequest
    {
        [Required]
        public int Id { get; set; }
    }
}