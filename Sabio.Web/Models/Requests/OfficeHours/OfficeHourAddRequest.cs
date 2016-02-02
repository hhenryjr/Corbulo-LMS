using Sabio.Web.Models.Requests.OfficeHours;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class OfficeHourAddRequest
    {
       
        public string UserId { get; set; }

        
        public int InstructorId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int StartTime { get; set; }

        [Required]
        public int EndTime { get; set; }

        [Required]
        public int TimeZone { get; set; }

        [Required]
        public string Topic { get; set; }

        [Required]
        public string Instructions { get; set; }

        [Required]
        public int SectionId { get; set; }

        public List<OfficeHourQuestionAddRequest> Questions { get; set; }
    }
}