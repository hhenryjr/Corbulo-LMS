using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Section
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CourseId { get; set; }

        public int DaysOfWeek { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime RegistrationDeadline { get; set; }

        public int StartTime { get; set; }

        public int EndTime { get; set; }

        public int TimeZone { get; set; }

        public string Info { get; set; }

        public int Format { get; set; }

        public int CampusLocation { get; set; }

        public string RoomNumber { get; set; }

        public int Capacity { get; set; }

        public int CurrentEnrollment { get; set; }

        public int Status { get; set; }

        public Campus Campus { get; set; }

        public CourseBase Course { get; set; }

        public List<SectionInstructors> Instructors { get; set; }

        public int EnrollmentStatusId { get; set; }

    }
}