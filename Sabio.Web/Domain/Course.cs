using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Course
    {
        public int Id { get; set; }

        public string CourseName { get; set; }

        public string Length { get; set; }

        public string Description { get; set; }

        public string TagName { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public List<CourseBase> Prereqs { get; set; }
        
        public List<CourseTag> Tags { get; set; }

        public int CourseLength { get; set; }

        public List<CourseInstructors> Instructors { get; set; }

        public string LearningObjectives { get; set; }

        public string ExpectedOutcome { get; set; }

        public string EvaluationCriteria { get; set; }

        public int Format { get; set; }

        //date added data modified


    }
}