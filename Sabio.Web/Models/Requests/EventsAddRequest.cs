using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class EventsAddRequest
    {
        [Required]
        public string EventName { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required]
        public string Date { get; set; }

        //[Range(typeof(DateTime), "1/1/2015", "6/6/2079")]
        //public DateTime Date { get; set; }

        [Required]
        public string PhoneNumber { get; set; }



        
    }
    
}