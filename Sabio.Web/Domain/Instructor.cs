using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Instructors

    {
        public int Id { get; set; }

        public string Name { get; set;}

        public string Email { get; set;}

        public string Bio { get; set; }

        public string LinkedIn { get; set;}

        public string CoursesTaught { get; set;}

        public DateTime DateAdded { get; set; }

        public DateTime DateModified { get; set; }

        public List<CourseBase> Courses { get; set; }

    }
}