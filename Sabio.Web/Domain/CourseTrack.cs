using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Web.Domain
{
    public class CourseTrack
    {
        public int Id { get; set; }

        public string CourseName { get; set; }

        public int TracksId { get; set; }
    }
}
