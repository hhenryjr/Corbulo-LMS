using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.Sections
{
    public class SectionDescriptionUpdate 
    {

        [Required]
        [MaxLength(4000)]
        public string Info { get; set; }
    }
}