using Sabio.Web.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.ViewModels
{
    public class SectionViewModel : BaseViewModel
    {
        public Dictionary<string, string> TimeZones { get; set; }

        public EnrollmentStatus EnrollmentStatus { get; set; }

        public CourseFormat CourseFormat { get; set;  }

        public WikiPageContentType WikiPageContentType { get; set; }
    }
}