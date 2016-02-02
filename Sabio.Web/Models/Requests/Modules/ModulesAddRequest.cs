using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Sabio.Web.Models.Requests
{
    public class ModuleAddRequest
    {        
        [Required]
        public string ModuleName { get; set; }
        
        public int SectionId { get; set; }
    }
}