using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Meeting
    {
        [Required]
        [Range(1, Int32.MaxValue)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(10, ErrorMessage = "Name must be at least 10 characters")]
        [MaxLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
        public string Name { get; set; }

        [MaxLength(250, ErrorMessage = "Description cannot be longer than 250 characters")]
        public string Description { get; set; }

        [Required]
        [Range(typeof(DateTime), "1/1/2015", "12/31/3000",
            ErrorMessage = "Value for {0} must be greater than {1}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm tt}")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm tt}")]
        public TimeSpan EndTime { get; set; }

        [Required]
        public int MeetingTypeId { get; set; }

        [Required]
        public int MeetingFormatId { get; set; }

        public bool Tentative { get; set; }

        public bool Public { get; set; }

        public List<int> MeetingLeaderPersonIds { get; set; }

        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }

    }
}