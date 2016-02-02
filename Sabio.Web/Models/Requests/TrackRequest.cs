using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Amazon.Runtime.Internal;
using Sabio.Web.Models.Requests.Track;

namespace Sabio.Web.Models.Requests
{
    public class TrackAddRequest 
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Format { get; set; }
        
        [Required]
        [MinLength(10)]
        public string ExpectedOutCome { get; set; }
       
        [Required]
        public int Cost { get; set; }

        [Required]
        public string Description { get; set; }

        public List<int> CourseIds { get; set; }

        public TrackAddRequest()
        {
            CourseIds = new List<int>();
        }

    }
}