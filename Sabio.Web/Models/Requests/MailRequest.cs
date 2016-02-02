using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class MailRequest
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public string FormEmail { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Subject Must Contain 2-150 Characters")]
        public string FormSubject { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Message Must Contain 10-2000 Characters")]
        public string FormMessage { get; set; }
     

    }
}