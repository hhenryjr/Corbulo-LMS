﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.CoursesRequest
{
    public class TrackCourseUpdate
    {
       
        [Required]
        [MaxLength(100)]
        public string CourseName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Length { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [MaxLength(4000)]
        public string LearningObjectives { get; set; }

        [Required]
        [MaxLength(4000)]
        public string ExpectedOutcome { get; set; }

        [Required]
        [MaxLength(4000)]
        public string EvaluationCriteria { get; set; }

        [Required]
        public int Format { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }
    }
}