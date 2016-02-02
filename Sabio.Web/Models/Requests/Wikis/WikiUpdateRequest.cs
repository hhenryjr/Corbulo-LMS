using Sabio.Web.Models.Requests.Wikis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class WikiUpdateRequest: WikiAddRequest
    {
        [Required]
        public int Id { get; set; }
    }
}


