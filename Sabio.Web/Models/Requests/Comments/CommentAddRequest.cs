using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class CommentAddRequest
    {
       

        [DataType(DataType.Text)]
        [Required]
        public int OwnerId { get; set; }

        [DataType(DataType.Text)]
        [Required]
        public string OwnerTypeId { get; set; }

        [DataType(DataType.Text)]
        [Required]
        public string Title { get; set; }

        [MinLength(8), MaxLength(3000)]
        [Required]
        public string Body { get; set; }

        [Required]
        public int ParentId { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime DateModified { get; set; }
    }
}