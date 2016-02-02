using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class SectionsAddRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int DaysOfWeek { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public DateTime RegistrationDeadline { get; set; }

        [Required]
        public int StartTime { get; set; }

        [Required]
        public int EndTime { get; set; }

        [Required]
        public int TimeZone { get; set; }

        [Required]
        public int Format { get; set; }

        [Required]
        public int Capacity { get; set; }

        public int CampusId { get; set; }

        public string RoomNumber { get; set; }

        //[Required]
        public int Status { get; set; }

        public List<string> EnrolledUsers { get; set; }

    }
}