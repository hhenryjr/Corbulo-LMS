using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class OfficeHourQuestion
    {
        public int Id { get; set; }

        public int OfficeHourId { get; set; }

        public string Question { get; set; }

        public string Response { get; set; }

        public string Grouping { get; set; }

        public int QuestionStatusId { get; set; }

        public List<Tag> Tags { get; set; }

    }
}