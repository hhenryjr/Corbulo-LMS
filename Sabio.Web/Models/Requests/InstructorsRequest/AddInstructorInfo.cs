using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class AddInstructorInfo
    {
        //public string UserId { get; set; }
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(5)]
        public string Bio { get; set; }
        [Required]
        [MinLength(10)]
        public string LinkedIn { get; set; }
        [Required]
        public List<string> CoursesTaught { get; set; }






    }
}