using Sabio.Web.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.OfficeHours
{
    public class OfficeHourQuestionAddRequest
    {
        
        public int OfficeHourId { get; set; }

        public string Question { get; set; }

        public string Response { get; set; }

        public string Grouping { get; set; }
        
        public int QuestionStatusId { get; set; }

        public List<Tag> Tag { get; set; }

    }
}