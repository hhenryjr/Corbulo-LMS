using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class CourseInstructors
    {
        public int CourseId { get; set; }
        public int InstructorId { get; set; }
        public virtual string Name { get; set; }
    }
}