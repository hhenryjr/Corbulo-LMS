using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace Sabio.Web.Models.Requests.Wikis
{
    public class WikiAddRequest

    {      
        [Required]
        [MinLength(2)]
        public string Title { get; set; }

        [Required]
        public string URL { get; set; }

        public int[] WikiSpaceIds { get; set; }

        public int ParentId { get; set; }
        
        public string LastModifiedUserId { get; set; }

        public List<int> Tags { get; set; }
    }
}