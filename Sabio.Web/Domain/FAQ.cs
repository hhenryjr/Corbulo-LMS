using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class FAQ
    {

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Must choose an option")]
        public int Id { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Must choose an option")]
        public int CategoryId { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Must choose an option")]
        public int LanguageId { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 10, ErrorMessage = "Question must be between 10 and 50 characters")]
        public string Question { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Answer must be between 10 and 50 characters")]
        public string Answer { get; set; }
    }
}