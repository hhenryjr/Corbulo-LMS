using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests.Meeting
{
    public class MeetingUpdateRequest : MeetingAddRequest
    {
        public int Id { get; set; }
    }
}