using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Sabio.Web.Models.Requests
{
    public class ModulesGetRequest<T>
    {
        [Required]
        public List<T> Items { get; set; }
    }
}