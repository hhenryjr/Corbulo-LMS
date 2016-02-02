using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class FAQUpdateRequest:FAQAddRequest

    {
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Must choose an option")]
        public int Id { get; set; }

    }
}