using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Module
    {
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public int Length { get; set; }
        public string Labs {get; set;}
        public string RequiredReading { get; set; }
        public string Homework { get; set; }
        public string Description { get; set; }
        public int SectionId { get; set; }
       
        
    }
}