using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Sabio.Web.Domain;

namespace Sabio.Web.Models.Requests.Testimonials
{
    public class TestimonialsAddRequest
    {

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public int StarRating { get; set; }
    }
}