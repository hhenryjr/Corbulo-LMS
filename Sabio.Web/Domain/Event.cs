using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Event
    {
        public int Id { get; set; }

        public string EventName { get; set; }

        public string Venue { get; set; }
     
        public string Date { get; set; }

        public string PhoneNumber { get; set; }
    }
}