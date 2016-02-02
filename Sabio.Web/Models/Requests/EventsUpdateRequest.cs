using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Request
{
    public class EventsUpdateRequest : EventsAddRequest

    {
        public int Id { get; set; }

    }
}



