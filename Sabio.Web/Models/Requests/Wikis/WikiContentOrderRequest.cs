using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.Wikis
{
    public class WikiContentOrderRequest
    {
        [Required]
        public List<WikiContentOrder> Content { get; set; }
    }
}