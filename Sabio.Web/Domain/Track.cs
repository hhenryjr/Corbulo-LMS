using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Track
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Format { get; set; }

        public string ExpectedOutcome { get; set; }

        public int Cost { get; set; }

        public string Description { get; set; }

        public List<TrackCourse> TracksCourses { get; set; }

        public List<int> CourseIds { get; set; }

        public List<CourseTrack> Prerequisites { get; set; } 

        public bool UserLoggedIn { get; set; }

        public Track()
        {
            // TrackDomain = new List<TrackCourse>();
        }
    }

}