using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sabio.Web.Domain
{
    public class Testimonials
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]     
        public string Body { get; set; }

        [Required]
        public int StarRating { get; set; }

    }
}