using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class CMSAddRequest
    {

        public string UserId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [DataType(DataType.Text)]
        [Required]
        public string Name { get; set; }

        [DataType(DataType.Url)]
        [Required]
        public string URL { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime DateToPublish { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime DateToExpire { get; set; }

        [MinLength(8), MaxLength(255)]
        [Required]
        public string Message { get; set; }
    }
}