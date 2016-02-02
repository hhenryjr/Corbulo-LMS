using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.UsersData
{
    public class UserStatusUpdate
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int SectionId { get; set; }

        [Required]
        public int EnrollmentStatusId { get; set; }
    }
}