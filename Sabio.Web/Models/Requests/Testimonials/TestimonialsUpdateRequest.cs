using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Sabio.Web.Domain;

namespace Sabio.Web.Models.Requests.Testimonials
{
    public class TestimonialsUpdateRequest : TestimonialsAddRequest
    {
        [Required]
        public int Id { get; set; }
    }
}