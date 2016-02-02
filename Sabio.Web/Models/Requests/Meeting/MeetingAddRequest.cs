using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.Meeting
{
    public class MeetingAddRequest
    {
       
        [Required(ErrorMessage="Name is required")]
        [MinLength(10, ErrorMessage = "Name must be at least 10 characters")]
        [MaxLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        [Range(typeof(DateTime), "1/1/2015", "12/31/3000",
            ErrorMessage = "Value for {0} must be greater than {1}")]
        public DateTime Date { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int MeetingTypeId { get; set; }

        [Required]
        public int MeetingFormatId { get; set; }

        public bool Tentative { get; set; }

        public bool Public { get; set; }

        public List<int> MeetingLeaderPersonIds { get; set; }

    }
}