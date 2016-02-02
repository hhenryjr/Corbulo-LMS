using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class OfficeHour
    {

        public int Id { get; set;}
       
        public string UserId { get; set; }

        public int InstructorId { get; set; }

        public DateTime Date { get; set; }

        public int StartTime { get; set; }

        public int EndTime { get; set; }

        public int TimeZone { get; set; }

        public string Topic { get; set; }

        public string Instructions { get; set; }

        public int SectionId { get; set; }

        public List<OfficeHourQuestion> Questions { get; set; }

    }
}