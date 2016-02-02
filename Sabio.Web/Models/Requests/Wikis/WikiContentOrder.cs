using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.Wikis
{
    public class WikiContentOrder
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Order { get; set; }
    }
}